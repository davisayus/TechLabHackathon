using LT.TechLabHackathon.Shared.DTOs;

namespace LT.TechLabHackathon.UI.Core
{
    public class CodeEditorCore
    {
        private ProgrammingLanguageDto _programmingLanguage;
        private List<string> _reservedWords;

        public CodeEditorCore()
        {
            _programmingLanguage ??= new ProgrammingLanguageDto();
            _reservedWords = new List<string>();
        }

        public void SetProgrammingLanguage(ProgrammingLanguageDto programmingLanguage)
        {
            _programmingLanguage = programmingLanguage;
            _reservedWords = programmingLanguage.ReservedWords.Select(r=>r.ReservedWord).ToList();
            _reservedWords.AddRange(programmingLanguage.DataTypes.Select(dt=>dt.DataType.Name).ToList());
        }

        public (string newHtmlCode, bool changed) MarkReservedWords(string htmlCodeEditor)
        {
            try
            {
                if (string.IsNullOrEmpty(htmlCodeEditor)) return (string.Empty, false);
                string htmlCodeWithReservedWords = htmlCodeEditor;
                string clearHtmlCode = htmlCodeEditor;

                var listWords = clearHtmlCode
                    .Replace("class=\"span_color\"", "")
                    .Replace("<p>", " ")
                    .Replace("</p>", " ")
                    .Replace("<blockquote style=\"margin: 0 0 0 40px; border: none; padding: 0px;\">", " ")
                    .Replace("</blockquote>", " ")
                    .Replace("&nbsp;", " ")
                    .Replace("(", " ")
                    .Replace(")", " ")
                    .Split(' ');

                foreach (var word in listWords.Where(x => x.Length > 0))
                    if (_reservedWords.Contains(word))
                        htmlCodeWithReservedWords = htmlCodeWithReservedWords.Replace(word, $"<span class=\"span_color\" style=\"color:#0000ff\">{word}</span>");

                return (htmlCodeWithReservedWords, (htmlCodeWithReservedWords != htmlCodeEditor));

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return (htmlCodeEditor, false);
            }

        }
    }
}
