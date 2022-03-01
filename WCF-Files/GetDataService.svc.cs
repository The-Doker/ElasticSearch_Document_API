using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCF_Files.Contexts;
using WCF_Files.Models;

namespace WCF_Files
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IGetDataService
    {
        public List<string> GetData()
        {
            var returnedList = new List<string>()
        {
            "doc",
            "docx",
            "pdf",
            "rtf",
            "xml"
        };
            try
            {
                var authDataFormContext = (ServerAuthContext)OperationContext.Current.IncomingMessageProperties["CurrentContext"];
                var checkAuth = new AccountModel();
                if (checkAuth.Login(authDataFormContext.userName, authDataFormContext.password))
                {
                    return returnedList;
                }
                else throw new Exception("Invalid account");
            }
            catch
            {
                throw new Exception("Unexpected error"); ;
            }
        }
    }
}
