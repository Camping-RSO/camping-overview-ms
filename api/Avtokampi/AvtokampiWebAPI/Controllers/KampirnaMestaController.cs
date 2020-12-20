using AvtokampiWebAPI.Models;
using AvtokampiWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AvtokampiWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class KampirnaMestaController : ControllerBase
    {
        private readonly IKampirnaMestaRepository _kampirnaMestaService;
        private readonly ILogger _logger;

        public KampirnaMestaController(IKampirnaMestaRepository kampirnaMestaService, ILogger<KampirnaMestaController> logger)
        {
            _kampirnaMestaService = kampirnaMestaService;
            _logger = logger;
        }

        /// <summary>
        ///     Seznam kampirnih mest za avtokamp
        /// </summary>
        /// <remarks>
        /// Primer zahtevka:
        ///
        ///     GET api/kampirnamesta/123/avtokamp
        ///
        /// </remarks>
        /// <returns>Seznam vseh kampirnih mest avtokampa</returns>
        /// <param name="kamp_id">Identifikator kampa</param>
        /// <response code="200">Seznam Kampirnih mest za podani avtokamp</response>
        /// <response code="400">Bad request error message</response>
        /// <response code="404">Not found error message</response>
        [HttpGet("{kamp_id}/avtokamp")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllKampMesta(int kamp_id)
        {
            try
            {
                var result = await _kampirnaMestaService.GetKampirnoMestoByAvtokamp(kamp_id);
                if (result == null)
                {
                    return NotFound(/*new ErrorHandlerModel($"Kampirno mesto z ID { id }, ne obstaja.", HttpStatusCode.NotFound)*/);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError("GET all kampirnamesta Unhandled exception ...", e);
                return BadRequest(/*new ErrorHandlerModel(e.Message, HttpStatusCode.BadRequest)*/);
            }
        }

        /// <summary>
        ///     Podatki o posameznemu kampirnem mestu
        /// </summary>
        /// <remarks>
        /// Primer zahtevka:
        ///
        ///     GET api/kampirnamesta/1234/12
        ///
        /// </remarks>
        /// <returns>Objekt KampirnaMesta</returns>
        /// <param name="kamp_mesto_id">Identifikator kampirnega mesta</param>
        /// <response code="200">Kampirno mesto</response>
        /// <response code="400">Bad request error message</response>
        /// <response code="404">Not found error message</response>
        [HttpGet("{kamp_mesto_id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetKampMesto(int kamp_mesto_id)
        {
            try
            {
                var result = await _kampirnaMestaService.GetKampirnoMestoByID(kamp_mesto_id);
                if (result == null)
                {
                    return NotFound(/*new ErrorHandlerModel($"Kampirno mesto z ID { id }, ne obstaja.", HttpStatusCode.NotFound)*/);
                }
                return Ok(result);
            }
            catch (ArgumentException)
            {
                return BadRequest(/*new ErrorHandlerModel($"Argument ID { id } ni v pravilni obliki.", HttpStatusCode.BadRequest)*/);
            }
            catch (Exception e)
            {
                _logger.LogError("GET kampirnamesta Unhandled exception ...", e);
                return BadRequest(/*new ErrorHandlerModel(e.Message, HttpStatusCode.BadRequest)*/);
            }
        }

        /// <summary>
        ///     Dodajanje novega kampirnega mesta
        /// </summary>
        /// <remarks>
        /// Primer zahtevka:
        ///
        ///     POST api/kampirnamesta/123
        ///     {
        ///         "Naziv": "Ime kampirnamesta"
        ///     }
        ///
        /// </remarks>
        /// <returns>Boolean value, success or not</returns>
        /// <param name="kamp_mesto">Objekt s podatki o novem kampirnem mestu</param>
        /// <param name="kamp_id">Identifikator avtokampa</param>
        /// <response code="201">If successfully created: true or false</response>
        /// <response code="400">Bad request error message</response>
        /// <response code="404">Not found error message</response>
        [HttpPost("{kamp_id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateKampMesto([FromBody] KampirnaMesta kamp_mesto, int kamp_id)
        {
            try
            {
                var result = await _kampirnaMestaService.CreateKampirnoMesto(kamp_mesto, kamp_id);
                if (result == false)
                {
                    return NotFound(/*new ErrorHandlerModel($"Kampirno mesto z ID { id }, ne obstaja.", HttpStatusCode.NotFound)*/);
                }
                return Created("/kampirnamesta/id", result);
            }
            catch (ArgumentException)
            {
                return BadRequest(/*new ErrorHandlerModel($"Argument ID { id } ni v pravilni obliki.", HttpStatusCode.BadRequest)*/);
            }
            catch (Exception e)
            {
                _logger.LogError("CREATE kampirnamesta Unhandled exception ...", e);
                return BadRequest(/*new ErrorHandlerModel(e.Message, HttpStatusCode.BadRequest)*/);
            }
        }

        /// <summary>
        ///     Urejanje podatkov o kampirnem mestu
        /// </summary>
        /// <remarks>
        /// Primer zahtevka:
        ///
        ///     PUT api/kampirnamesta/1234/123
        ///     {
        ///         "Naziv": "Nov ime kampirnamesta"
        ///     }
        ///
        /// </remarks>
        /// <returns>Objekt KampirnoMesto</returns>
        /// <param name="kamp_mesto">Podatki popravljenega kampirnega mesta</param>
        /// <param name="kamp_id">Identifikator avtokampa</param>
        /// <param name="kamp_mesto_id">Identifikator kampirnega mesta</param>
        /// <response code="204">No content</response>
        /// <response code="400">Bad request error message</response>
        /// <response code="404">Not found error message</response>
        [HttpPut("{kamp_id}/{kampirno_mesto_id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateKampMesto([FromBody] KampirnaMesta kamp_mesto, int kamp_id, int kamp_mesto_id)
        {
            try
            {
                var result = await _kampirnaMestaService.UpdateKampirnoMesto(kamp_mesto, kamp_id, kamp_mesto_id);
                if (result == null)
                {
                    return NotFound(/*new ErrorHandlerModel($"Kampirno mesto z ID { id }, ne obstaja.", HttpStatusCode.NotFound)*/);
                }
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest(/*new ErrorHandlerModel($"Argument ID { id } ni v pravilni obliki.", HttpStatusCode.BadRequest)*/);
            }
            catch (Exception e)
            {
                _logger.LogError("UPDATE kampirnamesta Unhandled exception ...", e);
                return BadRequest(/*new ErrorHandlerModel(e.Message, HttpStatusCode.BadRequest)*/);
            }
        }

        /// <summary>
        ///     Brisanje kampirnega mesta
        /// </summary>
        /// <remarks>
        /// Primer zahtevka:
        ///
        ///     DELETE api/kampirnamesta/1234/123
        ///
        /// </remarks>
        /// <returns>Boolean value</returns>
        /// <param name="kamp_id">Identifikator avtokampa</param>
        /// <param name="kamp_mesto_id">Identifikator kampirnega mesta</param>
        /// <response code="204">No content</response>
        /// <response code="400">Bad request error message</response>
        /// <response code="404">Not found error message</response>
        [HttpDelete("{kamp_id}/{kamp_mesto_id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteKampMesto(int kamp_id, int kamp_mesto_id)
        {
            try
            {
                var result = await _kampirnaMestaService.RemoveKampMesto(kamp_id, kamp_mesto_id);
                if (result == false)
                {
                    return NotFound(/*new ErrorHandlerModel($"Kampirno mesto z ID { id }, ne obstaja.", HttpStatusCode.NotFound)*/);
                }
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest(/*new ErrorHandlerModel($"Argument ID { id } ni v pravilni obliki.", HttpStatusCode.BadRequest)*/);
            }
            catch (Exception e)
            {
                _logger.LogError("DELETE kampirnamesta Unhandled exception ...", e);
                return BadRequest(/*new ErrorHandlerModel(e.Message, HttpStatusCode.BadRequest)*/);
            }
        }

        /// <summary>
        ///     Seznam kategorij kampirnih mest
        /// </summary>
        /// <remarks>
        /// Primer zahtevka:
        ///
        ///     GET api/kampirnamesta/kategorije
        ///
        /// </remarks>
        /// <returns>Seznam vseh kategorij kampirnih mest</returns>
        /// <response code="200">Seznam kategorij</response>
        /// <response code="400">Bad request error message</response>
        /// <response code="404">Not found error message</response>
        [HttpGet("kategorije")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetKategorijeKampirnihMest()
        {
            try
            {
                var result = await _kampirnaMestaService.GetKategorijeKampirnihMest();
                if (result == null)
                {
                    return NotFound(/*new ErrorHandlerModel($"Kategorija z ID { id }, ne obstaja.", HttpStatusCode.NotFound)*/);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError("GET GetKategorijeKampirnihMest Unhandled exception ...", e);
                return BadRequest(/*new ErrorHandlerModel(e.Message, HttpStatusCode.BadRequest)*/);
            }
        }
    }
}