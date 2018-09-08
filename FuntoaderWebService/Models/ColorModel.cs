using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuntoaderWebService.Models
{
    public class ColorModel : CommandModel
    {
        public Int32 argbValue { get; private set; }
        private const string argbCmd = "ARGB.";
        private const string displaySettingCmd = "t.";
        private const string colorCountCmd = "CC.";
        private const string colorCmd = "C.";
        private const CommandType type = CommandType.COLOR;
        private ColorDisplaySetting displaySetting;

        public ColorModel(Int32 argb, ColorDisplaySetting setting): base(type)
        {
            argbValue = argb;
            displaySetting = setting;
            CreateMessage(argbValue.ToString());           
        }

        public override void CreateMessage(string value)
        {
            string cmd;
            switch (displaySetting)
            {
                case ColorDisplaySetting.Cut:
                    cmd = colorCmd + argbCmd + argbValue.ToString() + fieldDelimeter + displaySettingCmd + 0;
                    break;
                case ColorDisplaySetting.Disolve:
                    cmd = colorCmd + argbCmd + argbValue.ToString() + fieldDelimeter + displaySettingCmd + 1;
                    break;
                default:
                    cmd = colorCmd + argbCmd + argbValue.ToString() + fieldDelimeter + displaySettingCmd + 0;
                    break;
            }
            message = new MessageModel(cmd);
        }
    }

    public enum ColorDisplaySetting
    {
        Cut,
        Disolve
    }
}