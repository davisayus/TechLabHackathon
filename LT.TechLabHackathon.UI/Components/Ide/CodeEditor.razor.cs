using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.UI.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;
using System.Net.Http.Headers;

namespace LT.TechLabHackathon.UI.Components.Ide
{
    public partial class CodeEditor
    {
        [Parameter] public ProgrammingLanguageDto ProgrammingLanguage { get; set; }
        [Parameter] public ChallengeDto Challenge { get; set; }
        [Parameter] public EventCallback<string> ResultCompilation { get; set; }


        private CodeEditorCore _core;

        Radzen.Blazor.RadzenHtmlEditor htmlEditor;
        string htmlCodeEditor = string.Empty;
        //"<p> <span style=\"color:#0000ff\">public</span> <span style=\"color:#0000ff\">class</span> Test <p>{</p> <p>}</p></p>";


        protected override void OnParametersSet()
        {

            if (ProgrammingLanguage.ProgrammingLanguageId != 0)
                _core.SetProgrammingLanguage(ProgrammingLanguage);
            
            if (Challenge.ChallengeId != 0 && string.IsNullOrEmpty(htmlCodeEditor))
            {
                string signature = Challenge.LanguageSignatures?.Where(ls => ls.ProgrammingLanguageId == ProgrammingLanguage.ProgrammingLanguageId).FirstOrDefault(new ChallengeLanguageSignatureDto()).Signature ?? string.Empty;
                if (!string.IsNullOrEmpty(signature))
                {
                    var (newHtmlCode, changed) = _core.MarkReservedWords(signature);
                    if (changed)
                        htmlCodeEditor = newHtmlCode;
                    else
                        htmlCodeEditor = signature;
                }

            }

            base.OnParametersSet();
        }

        public CodeEditor()
        {
            ProgrammingLanguage ??= new();
            Challenge ??= new();
            _core = new CodeEditorCore();
        }


        void OnPaste(HtmlEditorPasteEventArgs args)
        {
        }

        void OnChange(string html)
        {
        }

        [JSInvokable("OnInput")]
        public async Task OnInput(string html)
        {
            var cambios = EncontrarCambios(htmlCodeEditor, html);
            if (cambios.Any(x => x.Contains("&nbsp; ")))
            {
                try
                {
                    var (newHtmlCode, changed) = _core.MarkReservedWords(html);
                    if (changed)
                    {
                        htmlCodeEditor = newHtmlCode;
                        await jsRuntime.InvokeVoidAsync("setContent", htmlEditor.Element, htmlCodeEditor);
                        await jsRuntime.InvokeVoidAsync("moveCursorToEnd", "editor");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: OnInput ({ex.Message})");
                }
            }
        }

        private async Task OnExecute(HtmlEditorExecuteEventArgs args)
        {
            Console.WriteLine($"Execute: {args.CommandName}");
            if (args.CommandName.Contains("Play"))
            {
                var codeText = await jsRuntime.InvokeAsync<string>("getInnerText", htmlEditor.Element);
                var resultCompilation = await _progLanguageRepository.Compile(new Records.RequestCompile(Challenge, ProgrammingLanguage.ProgrammingLanguageId, codeText));
                await ResultCompilation.InvokeAsync(resultCompilation);
                Console.WriteLine(resultCompilation);
            }


        }

        public List<string> EncontrarCambios(string original, string changed)
        {
            List<string> cambios = [];

            try
            {
                var listStr1 = original
                    .Replace("<p>", " ")
                    .Replace("</p>", " ")
                    .Replace("<blockquote style=\"margin: 0 0 0 40px; border: none; padding: 0px;\">", "")
                    .Replace("</blockquote>", "")
                    .Split(' ');
                var listStr2 = changed
                    .Replace("<p>", " ")
                    .Replace("</p>", " ")
                    .Replace("<blockquote style=\"margin: 0 0 0 40px; border: none; padding: 0px;\">", "")
                    .Replace("</blockquote>", "")
                    .Split(' ');

                int words = Math.Max(listStr1.Length, listStr2.Length);

                for (int i = 0; i < words; i++)
                {
                    if (listStr1.Length > i && listStr2.Length > i && listStr1[i] != listStr2[i])
                        cambios.Add(listStr2[i] + " ");

                    if (listStr1.Length > i && listStr2.Length < i)
                        cambios.Add(listStr1[i]);

                    if (listStr2.Length > i && listStr1.Length < i)
                        cambios.Add(listStr2[i]);
                }

                cambios.Reverse();
                return cambios;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return [];
            }
            finally
            {

            }

        }

    }
}
