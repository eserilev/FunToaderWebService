using FuntoaderWebService.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace FuntoaderWebService.Services
{
    public class ColorService
    {
        private readonly CommandSender comandSender;

        public ColorService(CommandSender comandSender)
        {
            this.comandSender = comandSender;
        }
        public MessageModel SendArgbColorCommand(ColorRequest request)
        {
            
            ColorModel color;
            Int32 colorValue;

            if (request.rgba.Length < 3 || request.rgba.Length > 4) throw new Exception("Invalid rgba format");
    
            if(request.rgba.Length < 4)
            {
                colorValue = Color.FromArgb(request.rgba[0], request.rgba[1], request.rgba[2]).ToArgb();              
            }
            else
            {
                colorValue = Color.FromArgb(request.rgba[3], request.rgba[0], request.rgba[1], request.rgba[2]).ToArgb();        
            }
              
            switch (request.displaySetting)
            {
                case 1:
                    color = new ColorModel(colorValue, ColorDisplaySetting.Disolve);
                    break;
                default:
                    color = new ColorModel(colorValue, ColorDisplaySetting.Cut);
                    break;
            }
            return comandSender.SendMessage(color.message);
        }

        public MessageModel[] SendSelectedArgbCommand(ColorRequest[] requests)
        {
            MessageModel[] m = new MessageModel[requests.Length];
            for(int i = 0; i <requests.Length; i++)
            {
                var r = requests[i];
                m[i] = SendArgbColorCommand(r);
            }
            return m;
        }
    }
}