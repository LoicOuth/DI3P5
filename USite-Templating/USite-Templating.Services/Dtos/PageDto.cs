using System;
using System.Collections.Generic;

namespace USite_Templating.Services.Dtos
{
    public class PageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ElementDto> Elements { get; set; }
    }

    public class ElementDto
    {
        public Guid Id { get; set; }
        public TypeElement Type { get; set; }
        public string Content { get; set; }
        public int Position { get; set; }
        public string MenuId { get; set; }
        public string PageName { get; set; }
        public string Url { get; set; }
        public string Alt { get; set; }
        public List<ElementDto> ElementsChilds { get; set; }
        public List<StyleDto> Styles { get; set; }
    }

    public class StyleDto
    {
        public string Value { get; set; }
    }

    public enum TypeElement
    {
        Block,
        Image,
        H1,
        Button,
        Link
    }
}
