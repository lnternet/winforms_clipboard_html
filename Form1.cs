namespace winforms_clipboard_html;

using System.Windows.Forms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        
        var text = "Mr. Fluffles says Meow!";
        var image = Convert.ToBase64String(File.ReadAllBytes(@"./cat.png"));

        var content = @$"
            <div>{text}</div>
            <img src=""data:image/png;base64, {image}"" />
        ";

        var data = WrapHtmlContent(content);
    
        Clipboard.SetData("HTML Format", data);
    }

    public string WrapHtmlContent(string htmlContent)
    {
        string header = 
        @"Version:0.9
        StartHTML:AAAAAAAA
        EndHTML:BBBBBBBB
        StartFragment:CCCCCCCC
        EndFragment:DDDDDDDD";

        string htmlPrefix = "<html><body><!--StartFragment-->";
        string htmlSuffix = "<!--EndFragment--></body></html>";


        string data = header + "\n" + htmlPrefix + "\n" + htmlContent + "\n" + htmlSuffix;

        data = data.Replace("AAAAAAAA", String.Format("{0:D8}", data.IndexOf("<html>")));
        data = data.Replace("BBBBBBBB", String.Format("{0:D8}", data.IndexOf("</html>") + 6));
        data = data.Replace("CCCCCCCC", String.Format("{0:D8}", data.IndexOf("<!--StartFragment-->") + 20));
        data = data.Replace("DDDDDDDD", String.Format("{0:D8}", data.IndexOf("<!--EndFragment-->") - 1));

        return data;
    }
}
