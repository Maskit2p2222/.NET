using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using VinsUncoderLibrary.DataBase;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class VinWebService : WebService
{

    [WebMethod]
    public string GetModelsByMark(string mark)
    {
        List<string> models = VinDataBase.GetModelsByMark(mark);
        return JsonConvert.SerializeObject(models);
    }

    [WebMethod]
    public string GetMarksByMarkPart(string markPart)
    {
        List<string> marks = MarksDataBase.GetAllMarksByLike(markPart);
        return JsonConvert.SerializeObject(marks);
    }
}

