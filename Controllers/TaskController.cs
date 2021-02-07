#define RELEASE 

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.services.extension;

namespace WebApplication1.Controllers
{
    public class TaskController : ApiController
    {
        DB store;

        public TaskController()
        {
            store = new DB();
        }

        
        [HttpPost]
        [Route("task")]
        public async Task<HttpResponseMessage> CreateNewTask()
        {
            Tasks task = null;
            try
            {
                task = new Tasks();
                task.ID = Guid.NewGuid();
                task.Status = Models.TaskStatus.Created;
                task.LastTime = DateTime.Now;
#if RELEASE
                store.Tasks.Add(task);
                await store.SaveChangesAsync();
#endif

                return Request.CreateResponse(HttpStatusCode.Accepted, task.ID.ToString());
            }
            finally
            {
                if (task != null)
                {
                    task.Status = Models.TaskStatus.Running;
                    task.LastTime = DateTime.Now;
#if RELEASE
                    await store.SaveChangesAsync();
#endif

                    await FinishTaskAsync(task.ID);
                }
            }
        }


        [HttpGet]
        [Route("task/{id}")]
        public HttpResponseMessage GetExistTask(string id)
        {
            if (!Guid.TryParse(id, out Guid idGuid))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            Tasks task = null;
#if RELEASE
            task = store.Tasks.FirstOrDefault(x => x.ID == idGuid);
#endif
            if (task == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var result = JsonConvert.SerializeObject(new
            {
                Status = task.Status.GetCustomDescription(),
                Time = task.LastTime,
            });
            return Request.CreateResponse(HttpStatusCode.Accepted, result);
        }


        private async Task FinishTaskAsync(Guid id)
        {
            await Task.Delay(TimeSpan.FromMinutes(1));

            Tasks task = null;
#if RELEASE
            task = store.Tasks.FirstOrDefault(x => x.ID == id);
#endif
            if (task == null)
                return;
            task.Status = Models.TaskStatus.Finished;
            task.LastTime = DateTime.Now;
#if RELEASE
            await store.SaveChangesAsync();
#endif
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                store.Dispose();

            base.Dispose(disposing);
        }
    }
}

