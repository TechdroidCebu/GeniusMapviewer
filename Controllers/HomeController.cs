using GeniusMapViewer.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
//using System.Text.Json;


namespace GeniusMapViewer.Controllers
{
    public class HomeController : Controller
    {
        HttpClient httpClient;
        List<JobActivityModel> jobActivityList;
        List<NodeMasterModel> nodeMasterList;
        List<CU_TransModel> cuTransList;
        List<PhotoDiagramModel> photoList;
        List<MAT_TransModel> matTransList;
        List<Mat_trans_with_desc> matTransListWithDesc;
        List<NodeMasterWithNodeType> nodeMasterWithNodeType;
        List<Segment_Model> segmenMasterList;

        //string baseAPIUrl = "https://genius-api.aboitizpower.com/api/FixedAsset/"; //Dev OLD
        //string baseAPIUrl = "http://localhost:44335/api/FixedAsset/"; //Prod OLD


                       
        //string baseAPIUrl = "https://genius.aboitizpower.com/genius-api/"; //local NEW
        string baseAPIUrl = "https://genius.aboitizpower.com/genius-api-dev/"; //local Dev NEW


        //string baseAPIUrl = "http://localhost/genius-api/"; //Prod NEW
        //string baseAPIUrl = "http://localhost/genius-api-dev/"; //Prod Dev NEW


        public async Task<ActionResult> Index()
        {
            this.httpClient = new HttpClient();

            //if (jobActivityList?.Count > 0)
            //    return jobActivityList;

            // Online
            var url = baseAPIUrl + "api/JobActivityMasters";
            var response = await httpClient.GetStringAsync(url);
            if (!string.IsNullOrEmpty(response))
            {
                var json = "";
                //string content = await response.Content.ReadAsStringAsync();
                if (response.Contains('['))
                {
                    json = response;
                }
                else
                {
                    json = "[" + response + "]";
                }

                jobActivityList = JsonConvert.DeserializeObject<List<JobActivityModel>>(json);
            }
            return View(jobActivityList.OrderByDescending(x=>x.jaId));
        }


        public async Task<ActionResult> GetSegments(string SGG_ID)
        {
            this.httpClient = new HttpClient();
            List<NodeSegmentInfo> nodeSegmentInfos = new List<NodeSegmentInfo>();
            // Online
            var response = await httpClient.GetAsync(baseAPIUrl + "api/FixedAsset/GetSegmentsBySSGID?ssgid=" + SGG_ID);
            if (response.IsSuccessStatusCode)
            {
                var json = "";
                string content = await response.Content.ReadAsStringAsync();
                if (content.Contains('['))
                {
                    json = content;
                }
                else
                {
                    json = "[" + content + "]";
                }

                segmenMasterList = JsonConvert.DeserializeObject<List<Segment_Model>>(json);

               

                if (segmenMasterList.Count > 0)
                {
                    var cnt = 0;
                    var nodeId = "";
 
                    

                    foreach (var item in segmenMasterList)
                    {


                        List<string> node_from_to = new List<string>();



                        if(segmenMasterList.Count == 2)
                        {
                            if (cnt == (segmenMasterList.Count) - 1)
                            {
                                node_from_to.Add(item.nodeIdFrom);
                                
                            }
                            else
                            {
                                node_from_to.Add(item.nodeIdTo);
                                node_from_to.Add(item.nodeIdFrom);
                                
                            }
                        }
                        else
                        {
                            if (cnt == (segmenMasterList.Count))
                            {
                                node_from_to.Add(item.nodeIdFrom);

                            }
                            else
                            {
                                node_from_to.Add(item.nodeIdFrom);
                                node_from_to.Add(item.nodeIdTo);
                            }
                        }
                        

                        foreach (var item2 in node_from_to)
                        {
                            var responseNode = await httpClient.GetAsync(baseAPIUrl + "api/FixedAsset/GetNodeDetails?nodeId=" + item2);
                            if (responseNode.IsSuccessStatusCode)
                            {
                                var jsonNode = "";
                                string contentNode = await responseNode.Content.ReadAsStringAsync();
                                if (contentNode.Contains('['))
                                {
                                    jsonNode = contentNode;
                                }
                                else
                                {
                                    jsonNode = "[" + contentNode + "]";
                                }

                                if (contentNode.IndexOf("\"nodeId\":null") < 0)
                                {
                                    nodeMasterList = JsonConvert.DeserializeObject<List<NodeMasterModel>>(jsonNode);

                                    nodeSegmentInfos.Add(new NodeSegmentInfo
                                    {
                                        segmentId = item.segmentId,
                                        nodeIdFrom = item.nodeIdFrom,
                                        nodeIdTo = item.nodeIdTo,
                                        sagPercent = item.sagPercent,
                                        NodeInfo = nodeMasterList
                                    });
                                }

                            }

                        }


                        cnt +=1;
                    }


                    return Json(nodeSegmentInfos, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(nodeSegmentInfos, JsonRequestBehavior.AllowGet);
        }



        public async Task<ActionResult> GetNodes(string jaID)
        {
            this.httpClient = new HttpClient();

            // Online
            var response = await httpClient.GetAsync(baseAPIUrl + "api/FixedAsset/GetNodesByJaId?JaId=" + jaID);
            if (response.IsSuccessStatusCode)
            {
                var json = "";
                string content = await response.Content.ReadAsStringAsync();
                if (content.Contains('['))
                {
                    json = content;
                }
                else
                {
                    json = "[" + content + "]";
                }

                nodeMasterList = JsonConvert.DeserializeObject<List<NodeMasterModel>>(json);
            }

            return Json(nodeMasterList, JsonRequestBehavior.AllowGet);
        }

        //private readonly RestClient _client;
        public async Task<ActionResult> GetNodesWithNodeType(string jaID)
        {
            //this.httpClient = new HttpClient();

            //// Online
            //var response = await httpClient.GetAsync(baseAPIUrl + "GetNodesByJaId?JaId=" + jaID);
            //if (response.IsSuccessStatusCode)
            //{
            //    string json; 
            //    string content = await response.Content.ReadAsStringAsync();
            //    if (content.Contains('['))
            //    {
            //        json = content;
            //    }
            //    else
            //    {
            //        json = "[" + content + "]";
            //    }

            //    nodeMasterWithNodeType = System.Text.Json.JsonSerializer.Deserialize<List<NodeMasterWithNodeType>>(json);
            //}

            //return Json(nodeMasterWithNodeType.ToList(), JsonRequestBehavior.AllowGet);


            var _client = new RestClient(baseAPIUrl);
            var request = new RestRequest("api/FixedAsset/GetNodesByJaId?JaId=" + jaID);
            request.AddHeader("accept", "application/json");
            //request.AddHeader("content-type", "application/json; charset=UTF-8");
            request.Method = Method.Get;
            RestResponse response = _client.Execute(request);

            //var response = _client.Execute<NodeMasterWithNodeType>(request);
            //var res = JsonConvert.DeserializeObject<List<NodeMasterModel>>(response.ToString());

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }


        //GetCuTranByJaInstallationId
        public async Task<ActionResult> GetCuTranByJaInstallationId(string jaID)
        {

            this.httpClient = new HttpClient();

            // Online
            var response = await httpClient.GetAsync(baseAPIUrl + "api/FixedAsset/GetCuTranByJaInstallationId?jaInstallationId=" + jaID);
            if (response.IsSuccessStatusCode)
            {
                var json = "";
                string content = await response.Content.ReadAsStringAsync();
                if (content.Contains('['))
                {
                    json = content;
                }
                else
                {
                    json = "[" + content + "]";
                }

                cuTransList = JsonConvert.DeserializeObject<List<CU_TransModel>>(json);


            }


            return Json(cuTransList, JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> GetSegmentGroup(string jaID)
        {

            var _client = new RestClient(baseAPIUrl);
            var request = new RestRequest("api/FixedAsset/GetSegmentGroupsByJaId?JaId=" + jaID);
            request.AddHeader("accept", "application/json");
            //request.AddHeader("content-type", "application/json; charset=UTF-8");
            request.Method = Method.Get;
            RestResponse response = _client.Execute(request);

            //var response = _client.Execute<NodeMasterWithNodeType>(request);
            //var res = JsonConvert.DeserializeObject<List<NodeMasterModel>>(response.ToString());

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> GetEquipmentByJaID(string jaID)
        {

            var _client = new RestClient(baseAPIUrl);
            var request = new RestRequest("api/EqptMasters/GetEquipmentByJaId/" + jaID);
            request.AddHeader("accept", "application/json");
            //request.AddHeader("content-type", "application/json; charset=UTF-8");
            request.Method = Method.Get;
            RestResponse response = _client.Execute(request);

            //var response = _client.Execute<NodeMasterWithNodeType>(request);
            //var res = JsonConvert.DeserializeObject<List<NodeMasterModel>>(response.ToString());

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetCU(string nodeId)
        {
            this.httpClient = new HttpClient();

            // Online
            //var response = await httpClient.GetAsync(baseAPIUrl + "GetCuTranByNodeId?nodeId=" + nodeId);
            var response = await httpClient.GetAsync(baseAPIUrl + "api/FixedAsset/GetCuTranByReferenceId?refId=" + nodeId);
            if (response.IsSuccessStatusCode)
            {
                var json = "";
                string content = await response.Content.ReadAsStringAsync();
                if (content.Contains('['))
                {
                    json = content;
                }
                else
                {
                    json = "[" + content + "]";
                }

                cuTransList = JsonConvert.DeserializeObject<List<CU_TransModel>>(json);


            }


            return Json(cuTransList, JsonRequestBehavior.AllowGet);
        }



        public async Task<ActionResult> GetPhotos(string cuTransId)
        {
            this.httpClient = new HttpClient();

            // Online
            var response = await httpClient.GetAsync(baseAPIUrl + "api/FixedAsset/GetPhotosByCuTranId?cuTranId=" + cuTransId);
            if (response.IsSuccessStatusCode)
            {
                var json = "";
                string content = await response.Content.ReadAsStringAsync();
                if (content.Contains('['))
                {
                    json = content;
                }
                else
                {
                    json = "[" + content + "]";
                }

                photoList = JsonConvert.DeserializeObject<List<PhotoDiagramModel>>(json);


            }


            return Json(photoList, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> GetMatTrans(string cuTransId)
        {
            this.httpClient = new HttpClient();

            // Online
            var response = await httpClient.GetAsync(baseAPIUrl + "api/FixedAsset/GetMatTranByCuTranId?cuTranId=" + cuTransId);
            if (response.IsSuccessStatusCode)
            {
                var json = "";
                string content = await response.Content.ReadAsStringAsync();
                if (content.Contains('['))
                {
                    json = content;
                }
                else
                {
                    json = "[" + content + "]";
                }

                matTransList = JsonConvert.DeserializeObject<List<MAT_TransModel>>(json);


            }


            return Json(matTransList, JsonRequestBehavior.AllowGet);
        }







        public async Task<ActionResult> GetMatTransWithDescription(string cuTransId)
        {
            this.httpClient = new HttpClient();

            // Online
            var response = await httpClient.GetAsync(baseAPIUrl + "api/FixedAsset/GetMatTransWithDescription?cuId=" + cuTransId);
            if (response.IsSuccessStatusCode)
            {
                var json = "";
                string content = await response.Content.ReadAsStringAsync();
                if (content.Contains('['))
                {
                    json = content;
                }
                else
                {
                    json = "[" + content + "]";
                }

                matTransListWithDesc = JsonConvert.DeserializeObject<List<Mat_trans_with_desc>>(json);


            }


            return Json(matTransListWithDesc, JsonRequestBehavior.AllowGet);
        }








        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public ActionResult CreateJA(string jaNumber, string noOfJA)
        {
            return View();
        }

     
        public async Task<ActionResult> SaveJA(string jaNumber, int noOfJA)
        {

            this.httpClient = new HttpClient();
            StringContent content = new StringContent("", UnicodeEncoding.UTF8, "application/json");
            var url = baseAPIUrl + "api/JobActivityMasters/PostJobActivityMasterManual/{noOfJA}/{jaNumber}";
            var client2 = new HttpClient();
            var response2 = await client2.PostAsync(url, content);


            if (response2.IsSuccessStatusCode)
            {
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Duplicate Entry!", JsonRequestBehavior.AllowGet);
            }
        }



        //public async Task<List<JobActivityModel>> GetJobActivities()
        //{


        //}


    }
}
    public class NodeSegmentInfo
    {
        public string segmentId { get; set; }
        public string nodeIdFrom { get; set; }
        public string nodeIdTo { get; set; }
        public int sagPercent { get; set; }
        public IEnumerable<NodeMasterModel> NodeInfo { get;set; }   

    }