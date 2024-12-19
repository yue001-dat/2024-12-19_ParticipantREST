using _2024_12_19_ParticipantsLib;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _2024_12_19_ParticipantREST.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ParticipantController : ControllerBase
	{
		private ParticipantsRepository _participantsRepository;

		public ParticipantController(ParticipantsRepository _repo)
		{
			_participantsRepository = _repo;
		}

		// Get All Participants
		[EnableCors("AllowAll")]
		[HttpGet]
		public ActionResult<IEnumerable<Participant>> Get()
		{
			return _participantsRepository.GetAll();
		}

		// Get Participant by ID	
		[EnableCors("AllowAll")]
		[HttpGet("{id}")]
		public ActionResult<IEnumerable<Participant>> Get(int id)
		{
			Participant? participant = _participantsRepository.GetById(id);

			if(participant == null)
			{
				return NotFound();
			}

			return Ok(participant);
		}

		// Create a new Participant
		[EnableCors("SameOrigin")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPost]
		public ActionResult<Participant> Post([FromBody] Participant newParticipant)
		{
			try
			{
				Participant participant = _participantsRepository.Add(newParticipant);

				return Created("/" + participant.Id, participant);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// Delete Participant
		[EnableCors("SameOrigin")]
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<Participant> Delete(int id)
		{
			try
			{
				Participant? participant = _participantsRepository.GetById(id);

				if (participant == null)
				{
					return NotFound();
				}

				_participantsRepository.Delete(participant.Id);

				return Ok(participant);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}	
		}
	}
}
