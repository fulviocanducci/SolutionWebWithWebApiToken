using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAppFullFrameworkApi.Models;

namespace WebAppFullFrameworkApi.Controllers
{
    [Authorize]
    public class CommentsController : ApiController
    {
        public DatabaseContext DatabaseContext { get; }

        public CommentsController(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }
        
        public IQueryable<Comment> GetComment()
        {
            return DatabaseContext.Comment;
        }

        // GET: api/Comments/5
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> GetComment(int id)
        {
            Comment comment = await DatabaseContext.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/Comments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.Id)
            {
                return BadRequest();
            }

            DatabaseContext.Entry(comment).State = EntityState.Modified;

            try
            {
                await DatabaseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Comments
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> PostComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DatabaseContext.Comment.Add(comment);
            await DatabaseContext.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> DeleteComment(int id)
        {
            Comment comment = await DatabaseContext.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            DatabaseContext.Comment.Remove(comment);
            await DatabaseContext.SaveChangesAsync();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DatabaseContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return DatabaseContext.Comment.Count(e => e.Id == id) > 0;
        }
    }
}