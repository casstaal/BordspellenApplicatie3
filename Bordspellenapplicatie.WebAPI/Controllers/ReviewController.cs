using AutoMapper;
using Bordspellenapplicatie.WebAPI.Models.DTO;
using Bordspellenapplicatie.WebAPI.Models.Request.Review;
using Bordspellenapplicatie.WebAPI.Models.Response;
using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Bordspellenapplicatie.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IReviewRepository reviewRepository;

        public ReviewController(IMapper mapper, IReviewRepository reviewRepository)
        {
            this.mapper = mapper;
            this.reviewRepository = reviewRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ReviewsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Results<Ok<ReviewsResponse>, EmptyHttpResult, ProblemHttpResult>> GetReviews()
        {
            var reviewDomain = await reviewRepository.GetAll();

            return TypedResults.Ok(new ReviewsResponse() { Reviews = mapper.Map<IEnumerable<ReviewDto>>(reviewDomain) });
        }


        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Results<Ok<ReviewResponse>, EmptyHttpResult, ProblemHttpResult, NotFound>> GetReviewById([FromRoute] Guid id)
        {
            var review = await reviewRepository.GetById(id);

            if (review == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new ReviewResponse() { Review = mapper.Map<ReviewDto>(review) });
        }

        [HttpGet]
        [Route("/boardgamenight/{id:int}")]
        [ProducesResponseType(typeof(ReviewsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Results<Ok<ReviewsResponse>, EmptyHttpResult, ProblemHttpResult, NotFound>> GetReviewsByBoardGameNightId([FromRoute] int id)
        {
            var reviewDomain = await reviewRepository.GetByBoardGameNightId(id);

            if (reviewDomain == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new ReviewsResponse() { Reviews = mapper.Map<IEnumerable<ReviewDto>>(reviewDomain) });
        }

        [HttpGet]
        [Route("/author/{id:int}")]
        [ProducesResponseType(typeof(ReviewsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Results<Ok<ReviewsResponse>, EmptyHttpResult, ProblemHttpResult, NotFound>> GetReviewsByAuthorId([FromRoute] int id)
        {
            var reviewDomain = await reviewRepository.GetByAuthorId(id);

            if (reviewDomain == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new ReviewsResponse() { Reviews = mapper.Map<IEnumerable<ReviewDto>>(reviewDomain) });
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<NoContent, NotFound, ProblemHttpResult>> DeleteReview([FromRoute] Guid id)
        {
            var review = await reviewRepository.GetById(id);

            if (review == null)
            {
                return TypedResults.NotFound();
            }

            try
            {
                await reviewRepository.Delete(id);
                return TypedResults.NoContent();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<Created<ReviewResponse>, BadRequest, ProblemHttpResult>> CreateReview([FromBody] CreateReviewRequest request)
        {
            if (request == null)
            {
                return TypedResults.BadRequest();
            }

            var reviewDomainModel = mapper.Map<Review>(request);

            await reviewRepository.Add(reviewDomainModel);

            var response = new ReviewResponse
            {
                Review = mapper.Map<ReviewDto>(reviewDomainModel)
            };

            return TypedResults.Created($"https://localhost:7287/api/Review/{reviewDomainModel.Id}", response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Results<Ok<ReviewResponse>, NotFound, BadRequest, ProblemHttpResult>> UpdateReview([FromRoute] Guid id, [FromBody] UpdateReviewRequest request)
        {
            if (request == null)
            {
                return TypedResults.BadRequest();
            }

            var reviewDomainModel = mapper.Map<Review>(request);

            reviewDomainModel = await reviewRepository.Update(id, reviewDomainModel);

            if (reviewDomainModel == null)
            {
                return TypedResults.NotFound();
            }

            var response = new ReviewResponse
            {
                Review = mapper.Map<ReviewDto>(reviewDomainModel)
            };

            return TypedResults.Ok(response);
        }
    }
}
