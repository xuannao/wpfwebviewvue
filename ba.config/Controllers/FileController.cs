using ba.config.Models;
using ba.config.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace ba.valveguide.winapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("new")]
        public ResponseMessage New(object obj)
        {
            string[] keys = new[] { "title", "filter", "filename" };
            Dictionary<string, string> data = WebUtil.DaToDc(obj, keys);
            if (data.Count == 0) return ResponseMessage.Fail("");
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Title = data["title"].ToString(),
                Filter = data["filter"].ToString(),
                FileName = data["filename"].ToString()
            };
            var dialogres = dialog.ShowDialog() == true;
            if (dialogres)
            {
                return ResponseMessage.Success(dialog.FileName);
            }
            else
            {
                return ResponseMessage.Fail("");
            }
        }

        [HttpPost("open")]
        public ResponseMessage Open(object obj)
        {
            string[] keys = new[] { "title", "filter" };
            Dictionary<string, string> data = WebUtil.DaToDc(obj, keys);
            if (data.Count == 0) return ResponseMessage.Fail("");
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Title = data["title"],
                Filter = data["filter"],
            };
            var dialogres = dialog.ShowDialog() == true;
            if (dialogres)
            {
                return ResponseMessage.Success(dialog.FileName);
            }
            else
            {
                return ResponseMessage.Fail("");
            }
        }

        [HttpPost("save")]
        public ResponseMessage Save(object obj)
        {
            string[] keys = new[] { "path", "content" };
            Dictionary<string, string> data = WebUtil.DaToDc(obj, keys);
            if (data.Count == 0 || data["path"].IsNullOrEmpty())
            {
                return ResponseMessage.Fail("");
            }
            try
            {
                var path = System.IO.Path.GetDirectoryName(data["path"]);
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path!);
                }
                System.IO.File.WriteAllText(data["path"], data["content"]);
                return ResponseMessage.Success(true);
            }
            catch (Exception ex)
            {
                return ResponseMessage.Fail(ex.Message);
            }
        }

        [HttpPost("read")]
        public ResponseMessage Read(object obj)
        {
            string[] keys = new[] { "path" };
            Dictionary<string, string> data = WebUtil.DaToDc(obj, keys);
            if (data.Count == 0 || data["path"].IsNullOrEmpty())
            {
                return ResponseMessage.Fail("");
            }
            if (!System.IO.File.Exists(data["path"]))
            {
                var path = System.IO.Path.GetDirectoryName(data["path"]);
                System.IO.Directory.CreateDirectory(path!);
                using (System.IO.File.Create(data["path"])) { }
            }
            string content = System.IO.File.ReadAllText(data["path"]);
            return ResponseMessage.Success(content);
        }

        [HttpPost("name")]
        public ResponseMessage Name(object obj)
        {
            string[] keys = new[] { "path" };
            Dictionary<string, string> data = WebUtil.DaToDc(obj, keys);
            if (data.Count == 0)
            {
                return ResponseMessage.Fail("");
            }
            var res = System.IO.Path.GetFileNameWithoutExtension(data["path"]);
            return ResponseMessage.Success(res);
        }

        [HttpPost("ext")]
        public ResponseMessage Ext(object obj)
        {
            string[] keys = new[] { "path" };
            Dictionary<string, string> data = WebUtil.DaToDc(obj, keys);
            if (data.Count == 0)
            {
                return ResponseMessage.Fail("");
            }
            var res = System.IO.Path.GetExtension(data["path"]);
            return ResponseMessage.Success(res);
        }

        [HttpPost("path")]
        public ResponseMessage Path(object obj)
        {
            string[] keys = new[] { "path" };
            Dictionary<string, string> data = WebUtil.DaToDc(obj, keys);
            if (data.Count == 0)
            {
                return ResponseMessage.Fail("");
            }
            var res = System.IO.Path.GetDirectoryName(data["path"]);
            return ResponseMessage.Success(res);
        }

        [HttpPost("combine")]
        public ResponseMessage Combine(object obj)
        {
            string[] keys = new[] { "path1", "path2", "path3", "path4" };
            Dictionary<string, string> data = WebUtil.DaToDc(obj, keys);
            if (data.Count == 0)
            {
                return ResponseMessage.Fail("");
            }
            var res = System.IO.Path.Combine(data["path1"], data["path2"], data["path3"], data["path4"]);
            return ResponseMessage.Success(res);
        }
    }
}