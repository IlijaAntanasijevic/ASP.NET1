using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Common
{
    public class OpenAISettings
    {
        public string ApiKey {  get; set; }
        public string Model { get; set; }
        public string Prompt { get; set; }
        public string PromptSrb { get; set; }

    }
}
