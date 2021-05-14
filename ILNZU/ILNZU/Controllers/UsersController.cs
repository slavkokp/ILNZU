namespace ILNZU.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using DAL.Data;
    using DAL.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// User controller.
    /// </summary>
    public class UsersController : Controller
    {
        private readonly ILNZU_dbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="context">DB context.</param>
        public UsersController(ILNZU_dbContext context)
        {
            this.context = context;
        }

        // GET: Users

        /// <summary>
        /// Shows a view.
        /// </summary>
        /// <returns>A view.</returns>
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.User.ToListAsync());
        }

        // GET: Users/Details/5

        /// <summary>
        /// Shows details.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>A view.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var user = await this.context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return this.NotFound();
            }

            return this.View(user);
        }

        // GET: Users/Create

        /// <summary>
        /// Creates something.
        /// </summary>
        /// <returns>A view.</returns>
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        /// <summary>
        /// Creates something.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>A view of profile.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Username,Password,Name,Surname,ProfilePicture,Salt")] User user)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(user);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(user);
        }

        // GET: Users/Edit/5

        /// <summary>
        /// Edits a profile.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>A view of user profile.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var user = await this.context.User.FindAsync(id);
            if (user == null)
            {
                return this.NotFound();
            }

            return this.View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        /// <summary>
        /// Edits a profile.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <param name="user">User.</param>
        /// <returns>View of profile.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Username,Password,Name,Surname,ProfilePicture,Salt")] User user)
        {
            if (id != user.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(user);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.UserExists(user.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(user);
        }

        // GET: Users/Delete/5

        /// <summary>
        /// Deletes something.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>A view.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var user = await this.context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return this.NotFound();
            }

            return this.View(user);
        }

        // POST: Users/Delete/5

        /// <summary>
        /// Deletes confirmed.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>A view.</returns>
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await this.context.User.FindAsync(id);
            this.context.User.Remove(user);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        /// <summary>
        /// Checks if user exists.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>If user exists.</returns>
        private bool UserExists(int id)
        {
            return this.context.User.Any(e => e.Id == id);
        }
    }
}
