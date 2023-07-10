using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using USite.Domain.Common;
using USite.Domain.Enums;

namespace USite.Application.Template.Commands.ImportHtmlCommand;

public record ImportTemplateBlockCommand(string Name, string Description, string Html) : IRequest<Unit>;

public class ImportTemplateBlockCommandHandler : IRequestHandler<ImportTemplateBlockCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _config;

    public ImportTemplateBlockCommandHandler(IApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<Unit> Handle(ImportTemplateBlockCommand request, CancellationToken cancellationToken)
    {
        var exist = await _context.Elements.AnyAsync(x => ((BlockElement)x) != null && ((BlockElement)x).IsTemplate && ((BlockElement)x).Name == request.Name);

        if (exist)
            throw new InvalidOperationException("A template exist with this name");

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(request.Html);

        var firstHtmlElement = htmlDoc.DocumentNode.FirstChild;

        var firstElement = HtmlToElement(firstHtmlElement, true);

        if (firstElement == null)
            throw new InvalidOperationException("HTML start with white space");

        var firstBlock = (BlockElement)firstElement;
        firstBlock.Name = request.Name;
        firstBlock.Description = request.Description;
        firstBlock.IsTemplate = true;

        _context.Elements.Add(firstElement);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private BaseElement? HtmlToElement(HtmlNode htmlNode, bool first = false)
    {
        BaseElement element;

        switch (htmlNode.Name)
        {
            case "div":
                element = new BlockElement(string.Empty, string.Empty, htmlNode.GetAttributeValue("data-position", 0));
                break;
            case "h1":
                element = new H1Element(htmlNode.InnerText, htmlNode.GetAttributeValue("data-position", 0));
                break;
            case "button":
                element = new ButtonElement(htmlNode.InnerText, htmlNode.GetAttributeValue("data-position", 0));
                break;
            case "a":
                element = new LinkElement(htmlNode.InnerText, htmlNode.GetAttributeValue("data-position", 0));
                break;
            case "img":
                string defaultUrl = _config.GetSection("AzureStorage")["DefaultImageUrl"] ?? throw new InvalidOperationException("AzureStorage DefaultImageUrl is not configured"); ;
                element = new ImageElement(htmlNode.GetAttributeValue("data-position", 0), htmlNode.GetAttributeValue("src", defaultUrl), htmlNode.GetAttributeValue("alt", "image_element"));
                break;
            case "#text":
                return null;
            default:
                throw new InvalidOperationException($"The element {htmlNode.Name} is not implemented");
        }

        if (first && !(element is BlockElement))
            throw new InvalidOperationException("The first element is not a div");

        element.Styles = ClassListToStyleList(htmlNode.GetClasses().ToList());
        var childs = htmlNode.ChildNodes.ToList();

        if (childs.Count > 0)
        {
            foreach (var child in childs)
            {
                var elementChild = HtmlToElement(child);

                if (elementChild != null)
                    element.ElementsChilds.Add(elementChild);
            }
        }

        return element;
    }

    public List<Style> ClassListToStyleList(List<string> classes)
    {
        var styleList = new List<Style>();

        StyleProperty ClassToStyleProperty(string className)
        {
            var textColorPattern = @"text-\[#.*\]";
            var backgroundColorPattern = @"bg-\[#.*\]";
            var widthPattern = @"w-\[.*\]";

            var paddingDirectionPattern = @"p[lrtb]-\[.*px\]";
            var paddingPattern = @"p-\[.*px\]";

            var marginDirectionPattern = @"m[lrtb]-\[.*px\]";
            var marginPattern = @"m-\[.*px\]";

            var justifyPattern = @"justify-.*";

            var flexDirectionPattern = @"flex-.*";

            var textAlignPattern = @"^text-(center|left|justify|right)$";
            var fontSizePattern = @"text-\[.*px\]";
            var textDecorationPattern = @"^(font-bold|italic|underline)$";

            var borderPattern = @"border-\[.*px\]";
            var borderColorPattern = @"border-\[#.*\]";

            var shadowPattern = @"^shadow-(none|sm|md|lg|xl|2xl)$";

            return className switch
            {
                string s when Regex.IsMatch(s, textColorPattern) => StyleProperty.TextColor,
                string s when Regex.IsMatch(s, backgroundColorPattern) => StyleProperty.BackgroundColor,
                string s when Regex.IsMatch(s, widthPattern) => StyleProperty.Width,
                string s when Regex.IsMatch(s, paddingPattern) || Regex.IsMatch(s, paddingDirectionPattern) => StyleProperty.Padding,
                string s when Regex.IsMatch(s, marginPattern) || Regex.IsMatch(s, marginDirectionPattern) => StyleProperty.Margin,
                string s when Regex.IsMatch(s, justifyPattern) => StyleProperty.JustifyContent,
                string s when Regex.IsMatch(s, flexDirectionPattern) => StyleProperty.FlexDirection,
                string s when Regex.IsMatch(s, textAlignPattern) => StyleProperty.TextAlign,
                string s when Regex.IsMatch(s, fontSizePattern) => StyleProperty.FontSize,
                string s when Regex.IsMatch(s, textDecorationPattern) => StyleProperty.TextDecoration,
                string s when Regex.IsMatch(s, borderPattern) => StyleProperty.Border,
                string s when Regex.IsMatch(s, borderColorPattern) => StyleProperty.BorderColor,
                string s when Regex.IsMatch(s, shadowPattern) => StyleProperty.Shadow,
                string s when s == "flex" => StyleProperty.Flex,
                _ => throw new InvalidOperationException($"Unknow class : {className}")
            };
        }

        foreach (var className in classes)
        {
            var styleProperty = ClassToStyleProperty(className);

            var existingStyle = styleList.FirstOrDefault(x => x.Property == styleProperty);

            if (existingStyle != null)
            {
                styleList.Remove(existingStyle);

                if (styleProperty == StyleProperty.Padding || styleProperty == StyleProperty.Margin|| styleProperty == StyleProperty.TextDecoration)
                {
                    existingStyle.Value += $" {className}";
                }
                else
                {
                    existingStyle.Value = className;
                }

                styleList.Add(existingStyle);
            }
            else
            {
                styleList.Add(new(styleProperty, className));
            }
        }

        return styleList;
    }
}
