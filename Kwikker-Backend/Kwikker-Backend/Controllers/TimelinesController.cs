using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Service.Contracts;
using Shared.RequestFeatures;
using System;
using System.Dynamic;
using System.Text.Json;

namespace Kwikker_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimelinesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public TimelinesController(IServiceManager service) => _service = service;
        //user's tweets only =>profiles
        [HttpGet("profile/{UserId:int}")]
        public async Task<IActionResult>GetUserTimeline(int UserId)
       {
            var timeline=await _service.TimelineService.GetUserTimeline(UserId);


            Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(timeline.data));
            return Ok(timeline.twts);
        }

        //randomized collection of tweets of followed users => home
        [HttpGet("followed/{UserId:int}")]
        public async Task<IActionResult> GetHomeTimeline(int UserId )
        {

            var timeline = await _service.TimelineService.GetHomeTimeline(UserId);
            return Ok(timeline);

           
        }

        [HttpGet("random/{UserId:int}")]
        public async Task<IActionResult> GetRandomTimeline(int UserId)
        {


            var randomTimeline = await _service.TimelineService.GetRandomTimeline(UserId);
            return Ok(randomTimeline);
        }
        [HttpGet("LikedTweets/{UserId:int}")]
        public async Task<IActionResult> GetLikedTweetsByUser(int UserId, [FromQuery] LikeParameters likeParameters)
        {
            var pagedLikedTweets = await _service.LikeService.GetUserLikedTweets(UserId, likeParameters, trackChanges: false);

                Response.Headers.Add("X-Pagination",
          JsonSerializer.Serialize(pagedLikedTweets.metaData));
            
          

            return Ok(pagedLikedTweets.likedTweets);
        }

    }
}
