using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatUAISDK.Responses
{
    public class CheckResultResponse
    {
        //状态码
        public int Status { get; set; }

     
        public List<Keys> Keys  { get; set; }
    }
    public class Keys
    {
        public string Key { get; set; }
    }

}
