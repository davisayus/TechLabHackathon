using LT.TechLabHackathon.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Primitives;
using Microsoft.JSInterop;
using Radzen;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace LT.TechLabHackathon.UI.Components.Ide
{
    public partial class DescriptionChallenge
    {

        [Parameter] public ChallengeDto Challenge { get; set; }

        Radzen.Blazor.RadzenHtmlEditor htmlEditor;
        string htmlCodeEditor = string.Empty;

        public DescriptionChallenge()
        {
            Challenge ??= new ChallengeDto();
        }

        protected override void OnParametersSet()
        {

            if (Challenge.ChallengeId != 0 && !string.IsNullOrEmpty(Challenge.Description))
            {
                StringBuilder instructions = new StringBuilder("<h3 style=\"color: #5e9ca0;\"><span style=\"color: #ff6600;\"><strong>Instructions:</strong></span></h3>");
                instructions.AppendLine(GetTextWithStyle(Challenge.Description));
                instructions.Append("<p></p>");
                instructions.Append(GetTextWithStyle($"Input parameters: {Challenge.InputParameters}"));

                foreach (var setupParameter in Challenge.InputSetupParameters)
                    instructions.Append(GetTextWithStyle($"Parameter Name: {setupParameter.ParameterName}, Type: {setupParameter.DataType.Name}, Sequence: {setupParameter.Sequence}"));
                instructions.Append(GetTextWithStyle($"Result Type: {Challenge.DataType.Name}"));

                instructions.Append("<h3 style=\"color: #5e9ca0;\"><span style=\"color: #ff6600;\"><strong>Example:</strong></span></h3>");
                foreach (var example in Challenge.Examples)
                {
                    var validation = Challenge.Validations.Where(v => v.ValidationId == example.ValidationId).FirstOrDefault(new ChallengeValidationDto());
                    foreach (var inputParameter in validation.InputParameters)
                    {
                        string paramName = Challenge.InputSetupParameters.Where(isp => isp.Sequence == inputParameter.Sequence).FirstOrDefault(new ChallengeInputSetupParameterDto()).ParameterName;
                        instructions.Append(GetTextWithStyle($"Input: {paramName} = {inputParameter.InputValue}"));
                    }
                    instructions.Append(GetTextWithStyle($"Result value: {validation.OutputValue}"));
                    instructions.Append($"<p style=\"color: #5e9ca0;\"><strong>Explanation:</strong><span style=\"color: #808080;\">{example.Explanation}</span></p>");
                    instructions.Append($"<p></p>");
                }
                instructions.Append("<h3 style=\"color: #5e9ca0;\"><span style=\"color: #ff6600;\"><strong>Constraints:</strong></span></h3>");
                instructions.Append("<ul>");
                foreach (var constraint in Challenge.Constraints)
                    instructions.Append($"<li style=\"color: #5e9ca0;\"><span style=\"color: #808080;\">{constraint.Description}</span></li>");
                instructions.Append("</ul>");

                htmlCodeEditor = instructions.ToString();
            }


            base.OnParametersSet();
        }

        static string GetTextWithStyle(string text)
        {
            return $"<p style=\"color: #5e9ca0;\"><span style=\"color: #808080;\">{text}</span></p>";
        }

        void OnPaste(HtmlEditorPasteEventArgs args)
        {
        }

        void OnChange(string html)
        {
        }

        public async Task OnInput(string html)
        {
        }

        async Task OnExecute(HtmlEditorExecuteEventArgs args)
        {
        }

    }
}
