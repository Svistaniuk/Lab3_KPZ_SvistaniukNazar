using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

class Program
{
    class HtmlDocument
    {
        public HtmlElement Body { get; set; }
    }

    class HtmlElement
    {
        public string TagName { get; set; }
        public string InnerText { get; set; }
        public HtmlElement Parent { get; set; }
        public HtmlElement NextSibling { get; set; }
        public HtmlElement PreviousSibling { get; set; }
        public HtmlElement FirstChild { get; set; }
        public HtmlElement LastChild { get; set; }

        public void AppendChild(HtmlElement element)
        {
            if (FirstChild == null)
            {
                FirstChild = element;
                LastChild = element;
            }
            else
            {
                LastChild.NextSibling = element;
                element.PreviousSibling = LastChild;
                LastChild = element;
            }
            element.Parent = this;
        }
    }

    static void Main()
    {
        string text = File.ReadAllText("input.txt");
        var document = new HtmlDocument();
        document.Body = new HtmlElement { TagName = "body" };

        int h1Counter = 0;
        foreach (var line in text.Split('\n'))
        {
            HtmlElement element;
            if (h1Counter == 0)
            {
                element = new HtmlElement { TagName = "h1" };
                h1Counter++;
            }
            else if (line.Length < 20)
            {
                element = new HtmlElement { TagName = "h2" };
            }
            else if (char.IsWhiteSpace(line[0]))
            {
                element = new HtmlElement { TagName = "blockquote" };
            }
            else
            {
                element = new HtmlElement { TagName = "p" };
            }
            element.InnerText = line;
            document.Body.AppendChild(element);
        }

        Console.WriteLine($"Використання пам'яті до оптимізації: {GC.GetTotalMemory(false)} байт");

        // Оптимізація з використанням GarbageCollector
        GC.Collect();

        Console.WriteLine($"Використання пам'яті після оптимізації: {GC.GetTotalMemory(false)} байт");

        Console.WriteLine("HTML-верстка:");
        Console.WriteLine(GetHtml(document));
    }

    static string GetHtml(HtmlDocument document)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<html>");
        sb.AppendLine("  <body>");
        GetHtml(document.Body, sb);
        sb.AppendLine("  </body>");
        sb.AppendLine("</html>");
        return sb.ToString();
    }

    static void GetHtml(HtmlElement element, StringBuilder sb)
    {
        sb.Append($"    <{element.TagName}>");
        sb.AppendLine(element.InnerText);
        sb.Append($"    </{element.TagName}>");
        if (element.NextSibling != null)
        {
            GetHtml(element.NextSibling, sb);
        }
    }
}