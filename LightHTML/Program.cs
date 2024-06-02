using System;
using System.Collections.Generic;
using System.Text;

namespace LightHTML
{
    // Базовий клас для всіх елементів розмітки
    public abstract class LightNode
    {
        public abstract string OuterHTML { get; }
        public abstract string InnerHTML { get; }
    }

    // Клас для текстових вузлів
    public class LightTextNode : LightNode
    {
        private readonly string _text;

        public LightTextNode(string text)
        {
            _text = text;
        }

        public override string OuterHTML => _text;
        public override string InnerHTML => _text;
    }

    // Типи відображення елементів
    public enum DisplayType
    {
        Block,
        Inline
    }

    // Типи закриття тегів
    public enum ClosingType
    {
        SelfClosing,
        Normal
    }

    // Клас для елементів розмітки
    public class LightElementNode : LightNode
    {
        private readonly string _tagName;
        private readonly DisplayType _displayType;
        private readonly ClosingType _closingType;
        private readonly List<string> _cssClasses = new List<string>();
        private readonly List<LightNode> _children = new List<LightNode>();

        public LightElementNode(string tagName, DisplayType displayType, ClosingType closingType)
        {
            _tagName = tagName;
            _displayType = displayType;
            _closingType = closingType;
        }

        public void AddClass(string className)
        {
            _cssClasses.Add(className);
        }

        public void AddChild(LightNode child)
        {
            _children.Add(child);
        }

        public override string OuterHTML
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append($"<{_tagName}");

                if (_cssClasses.Count > 0)
                {
                    sb.Append($" class=\"{string.Join(" ", _cssClasses)}\"");
                }

                if (_closingType == ClosingType.SelfClosing)
                {
                    sb.Append(" />");
                }
                else
                {
                    sb.Append(">");
                    foreach (var child in _children)
                    {
                        sb.Append(child.OuterHTML);
                    }
                    sb.Append($"</{_tagName}>");
                }

                return sb.ToString();
            }
        }

        public override string InnerHTML
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var child in _children)
                {
                    sb.Append(child.OuterHTML);
                }
                return sb.ToString();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення елементів розмітки
            LightElementNode div = new LightElementNode("div", DisplayType.Block, ClosingType.Normal);
            div.AddClass("container");

            LightElementNode header = new LightElementNode("h1", DisplayType.Block, ClosingType.Normal);
            header.AddChild(new LightTextNode("Welcome to LightHTML!"));

            LightElementNode paragraph = new LightElementNode("p", DisplayType.Block, ClosingType.Normal);
            paragraph.AddChild(new LightTextNode("This is an example paragraph."));

            LightElementNode link = new LightElementNode("a", DisplayType.Inline, ClosingType.Normal);
            link.AddClass("link");
            link.AddChild(new LightTextNode("Click here"));

            LightElementNode image = new LightElementNode("img", DisplayType.Inline, ClosingType.SelfClosing);
            image.AddClass("image");

            LightElementNode ul = new LightElementNode("ul", DisplayType.Block, ClosingType.Normal);
            ul.AddClass("item-list");

            LightElementNode li1 = new LightElementNode("li", DisplayType.Block, ClosingType.Normal);
            li1.AddClass("item");
            li1.AddChild(new LightTextNode("Item 1"));

            LightElementNode li2 = new LightElementNode("li", DisplayType.Block, ClosingType.Normal);
            li2.AddClass("item");
            li2.AddChild(new LightTextNode("Item 2"));

            ul.AddChild(li1);
            ul.AddChild(li2);

            div.AddChild(header);
            div.AddChild(paragraph);
            div.AddChild(link);
            div.AddChild(image);
            div.AddChild(ul);

            // Виведення розмітки в консоль
            Console.WriteLine(div.OuterHTML);
            Console.WriteLine("\nInner HTML:");
            Console.WriteLine(div.InnerHTML); 

            // Додатковий приклад з іншими елементами
            LightElementNode article = new LightElementNode("article", DisplayType.Block, ClosingType.Normal);
            article.AddClass("post");

            LightElementNode section = new LightElementNode("section", DisplayType.Block, ClosingType.Normal);
            section.AddClass("intro");
            section.AddChild(new LightTextNode("This is an introduction section."));

            article.AddChild(section);
            article.AddChild(new LightTextNode("More content goes here."));

            Console.WriteLine("\nAdditional example:");
            Console.WriteLine(article.OuterHTML);
        }
    }
}
