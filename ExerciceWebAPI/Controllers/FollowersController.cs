using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExerciceWebAPI.Models;
using ExerciceWebAPI.DAL;
using System.Runtime.InteropServices;

namespace ExerciceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        private readonly FollowerDbContext _context;

        public FollowersController(FollowerDbContext context)
        {
            _context = context;
        }

        // GET: api/Followers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Follower>>> GetFollowers()
        {
            return await _context.Followers.ToListAsync();
        }

        // GET: api/Followers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Follower>> GetFollower(Guid id)
        {
            var follower = await _context.Followers.FindAsync(id);

            if (follower == null)
            {
                return NotFound();
            }

            return follower;
        }

        //research all
        //https://localhost:44321/api/Followers/GetFollowerByResearch
        //research with both
        //https://localhost:44321/api/Followers/GetFollowerByResearch/bi/gat
        //research with FirstName only
        //https://localhost:44321/api/Followers/GetFollowerByResearch/bi
        //research without FirstName (%20 equivalent to spacekey pressed)
        //https://localhost:44321/api/Followers/GetFollowerByResearch/%20/gat

        [Route("[action]/{firstNameFilter?}/{lastNameFilter?}")]
        [HttpGet]
        public async Task <ActionResult<IEnumerable<Follower>>> GetFollowerByResearch([Optional]string firstNameFilter, [Optional]string lastNameFilter)
        {
            var followers = _context.Followers.AsQueryable();

            if(firstNameFilter != null)
            {
                followers = followers.Where(f => f.FirstName.ToLower().Contains(firstNameFilter.ToLower()));
            }

            if(lastNameFilter != null)
            {
                followers = followers.Where(f => f.LastName.ToLower().Contains(lastNameFilter.ToLower())); ;
            }

            return await followers.ToListAsync();
        }
        // PUT: api/Followers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollower(Guid id, Follower follower)
        {
            if (id != follower.ID)
            {
                return BadRequest();
            }

            _context.Entry(follower).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Followers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Follower>> PostFollower(Follower follower)
        {
            _context.Followers.Add(follower);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetFollower", new { id = follower.ID }, follower);
            return CreatedAtAction(nameof(GetFollower), new { id = follower.ID }, follower);
        }

        // DELETE: api/Followers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Follower>> DeleteFollower(Guid id)
        {
            var follower = await _context.Followers.FindAsync(id);
            if (follower == null)
            {
                return NotFound();
            }

            _context.Followers.Remove(follower);
            await _context.SaveChangesAsync();

            return follower;
        }

        private bool FollowerExists(Guid id)
        {
            return _context.Followers.Any(e => e.ID == id);
        }
    }
}
