using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using USite.Infrastructure.Identity;
using Site = USite.Domain.Entities.Site;
using Page = USite.Domain.Entities.Page;
using USite.Domain.Entities;
using USite.Domain.Enums;

namespace USite.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly CompleteDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, CompleteDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void Initialize()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                _context.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        //Default Role
        if (_roleManager.Roles.All(r => r.Name != RoleConstants.Administrator))
        {
            await _roleManager.CreateAsync(new IdentityRole(RoleConstants.Administrator));
        }
        if (_roleManager.Roles.All(r => r.Name != RoleConstants.User))
        {
            await _roleManager.CreateAsync(new IdentityRole(RoleConstants.User));
        }

        // Add User
        if (!_userManager.Users.Any(u => u.Email == "user@diiage.org"))
        {
            var user = new ApplicationUser("User", "Test") { Email = "user@diiage.org", UserName = "user@diiage.org" };

            await _userManager.CreateAsync(user, "Azerty@123");
            await _userManager.AddToRoleAsync(user, RoleConstants.User);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _userManager.ConfirmEmailAsync(user, code);

            user = await _userManager.FindByEmailAsync(user.Email);

            // Add Site
            var site = new Site(user.Id, "test Usite", "Notre site de test");
            var page = new Page("Home", "Home Page")
            {
                Site = site,
                IsFirst = true
            };
            _context.Pages.Add(page);
            await _context.SaveChangesAsync();

            // Add Block
            var createdPage = await _context.Pages
            .Select(x => new { Page = x, x.Elements })
            .FirstAsync(x => x.Page.Id == page.Id);

            var position = page.Elements.Count + 1;
            var block = new BlockElement("First block", "Test block", position)
            {
                Page = createdPage.Page,
                Styles = new List<Style>()
                {
                    new Style(StyleProperty.Flex, "flex"),
                }
            };
            _context.Elements.Add(block);
            await _context.SaveChangesAsync();

            // Add Text
            var blockCreated = await _context.Elements.FirstOrDefaultAsync(x => x.Id == block.Id);
            var count = await _context.Elements
              .Where(x => x.Id == blockCreated.Id)
              .SelectMany(x => x.ElementsChilds)
              .CountAsync();

            var entity = new H1Element("text", count + 1)
            {
                ParentId = blockCreated.Id
            };

            _context.Elements.Add(entity);
            await _context.SaveChangesAsync();
        }

    }
}