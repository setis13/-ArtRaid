using ArtRaid.Dtos;
using ArtRaid.WebServices.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ArtRaid.WebServices {
    public class ArtWebService {
        public async Task<WebResult<WebArtDto[]>> GetArts(int userId) {
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://tm.myautocall.net/Api/Home/GetArts?UserId={userId}");
                request.Method = "GET";
                request.ContentType = "application/json";
                using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {
                        var json = reader.ReadToEnd();
                        return JsonConvert.DeserializeObject<WebResult<WebArtDto[]>>(json);
                    }
                }
            } catch (Exception e) {
                return new WebResult<WebArtDto[]>() { Message = e.Message };
            }
        }

        public async Task<WebResult<WebArtDto>> AddArt(int userId, WebArtDto dto) {
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://tm.myautocall.net/Api/Home/AddArt?UserId={userId}");
                request.Method = "POST";
                request.ContentType = "application/json";
                using (var writer = new StreamWriter(await request.GetRequestStreamAsync())) {
                    writer.Write(JsonConvert.SerializeObject(dto));
                }
                using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {
                        return JsonConvert.DeserializeObject<WebResult<WebArtDto>>(reader.ReadToEnd());
                    }
                }
            } catch (Exception e) {
                return new WebResult<WebArtDto>() { Message = e.Message };
            }
        }

        public async Task<WebResult> DeleteArt(int userId, int artId) {
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://tm.myautocall.net/Api/Home/DeleteArt?UserId={userId}&Id={artId}");
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = 0;
                using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {
                        return JsonConvert.DeserializeObject<WebResult>(reader.ReadToEnd());

                    }
                }
            } catch (Exception e) {
                return new WebResult<List<WebArtDto>>() { Message = e.Message };
            }
        }

        public async Task<WebResult> DeleteArts(int userId, List<int> artIds) {
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://tm.myautocall.net/Api/Home/DeleteArts?UserId={userId}");
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = 0;
                using (var writer = new StreamWriter(await request.GetRequestStreamAsync())) {
                    writer.Write(JsonConvert.SerializeObject(artIds.ToArray()));
                }
                using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {
                        return JsonConvert.DeserializeObject<WebResult>(reader.ReadToEnd());
                    }
                }
            } catch (Exception e) {
                return new WebResult<List<WebArtDto>>() { Message = e.Message };
            }
        }
    }
}
