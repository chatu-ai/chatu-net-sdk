using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatUAISDK.Requests
{
    public class CreateImageRequest
    {    /// <summary>
         /// 提示词
         /// </summary>
        public string Prompt { get; set; }
        /// <summary>
        /// 风格(默认enhance)
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 张数（1-4）
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// 是否优化及翻译提示词
        /// </summary>
        public bool? PromptOptimize { get; set; }
    }
}
