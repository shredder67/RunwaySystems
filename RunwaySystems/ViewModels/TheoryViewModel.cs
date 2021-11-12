using MdXaml;
using System;
using System.Windows;
using System.Windows.Documents;

namespace RunwaySystems.ViewModels
{
    public class TheoryViewModel : ViewModel
    {
        public FlowDocument TheoryMarkDown { get; }

        public TheoryViewModel()
        {
            Markdown engine = new Markdown();
            try
            {
                string markdownTxt = System.IO.File.ReadAllText("pack://application:,,,/Resources/Theory.md");
                TheoryMarkDown = engine.Transform(markdownTxt);
            } catch(Exception e)
            {
                MessageBox.Show("Невозможно открыть файл теории, попробуйте снова");
            }
        }
    }
}
