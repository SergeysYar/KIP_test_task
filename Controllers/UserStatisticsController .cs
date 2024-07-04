using Microsoft.AspNetCore.Mvc;

namespace KIP_test_task.Controllers
{
    [ApiController]
    [Route("report")]
    public class UserStatisticsController : ControllerBase
    {
        private readonly UserStatisticsDbContext _context;

        public UserStatisticsController(UserStatisticsDbContext context)
        {
            _context = context;
        }

        [HttpPost("user_statistics")]
        public async Task<IActionResult> CreateUserStatisticsRequest(Guid userId, DateTime startDate, DateTime endDate)
        {
            var request = new UserStatisticsRequest
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                StartDate = startDate,
                EndDate = endDate,
                RequestTime = DateTime.UtcNow,
                IsCompleted = false,
                Result = null
            };

            _context.UserStatisticsRequests.Add(request);
            await _context.SaveChangesAsync();

            return Ok(request.Id);
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetRequestInfo(Guid query)
        {
            var request = await _context.UserStatisticsRequests.FindAsync(query);

            if (request == null)
            {
                return NotFound();
            }

            var elapsed = DateTime.UtcNow - request.RequestTime;
            var configTime = TimeSpan.FromSeconds(60); // 60 секунд
            var percent = (int)(elapsed.TotalMilliseconds / configTime.TotalMilliseconds * 100);
            percent = Math.Min(100, percent);

            var response = new
            {
                Query = request.Id,
                Percent = percent,
                Result = percent >= 100 ? new { request.UserId, CountSignIn = 12 } : null
            };

            return Ok(response);
        }
    }
}
    public class UserStatisticsRequestDto
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class UserStatisticsInfoDto
    {
        public Guid Query { get; set; }
        public int Percent { get; set; }
        public string Result { get; set; }
    }

