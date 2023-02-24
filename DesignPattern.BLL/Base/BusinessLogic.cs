﻿using DesignPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesignPattern.BLL.Base
{
    public interface IBusinessLogic : IDisposable
    {
        AppSettings AppSettings { get; }
    }

    public class BusinessLogic : IBusinessLogic
    {
        public AppSettings AppSettings { get; private set; }
        public IBusinessLogicFactory BusinessLogicFactory { get; private set; }
        public BusinessLogic(IBusinessLogicFactory businessLogicFactory)
        {
            if (AppSettings == null)
            {
                this.AppSettings = GetAppSettings();
            }

            this.BusinessLogicFactory = businessLogicFactory;
        }

        // 讀取設定檔
        public AppSettings GetAppSettings()
        {
            var settingFilePath = Path.Combine(Directory.GetCurrentDirectory(), AppSettings.FileName);

            if (!File.Exists(settingFilePath))
            {
                throw new FileNotFoundException($"Could not find {AppSettings.FileName}", AppSettings.FileName);
            }

            return JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(settingFilePath));
        }

        protected virtual void Dispose(bool Disposing)
        {
            // 讓子類別可覆寫釋放資源
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual TLogic CreateLogic<TLogic>()
        {
            return this.BusinessLogicFactory.GetLogic<TLogic>();
        }
    }
}
