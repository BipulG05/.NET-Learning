using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi2024.Data;
using WebApi2024.Dtos.Stock;
using WebApi2024.Helpers;
using WebApi2024.Interfaces;
using WebApi2024.Mappers;

namespace WebApi2024.Controllers
{
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepositoty _stockRepo;

        public StockController(ApplicationDBContext context, IStockRepositoty stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }
        /**

        [HttpGet]
        public IActionResult GetAll()
        {
            //var stock = _context.Stocks.ToList().Select(s => s.ToStockDto());
            var stock = _context.Stocks
                        .AsNoTracking()
                        .Select(s => s.ToStockDto())
                        .ToList();
            return Ok(stock);
        }
        [HttpGet("{id}")]
        public IActionResult GetbyId([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockmodel = stockDto.ToStockFromCreateDTO();
            _context.Stocks.Add(stockmodel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetbyId), new { id = stockmodel.Id }, stockmodel.ToStockDto());

        }
        [HttpPut]
        [Route("{id}")]
        //[HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest("Update data is required.");
            }

            var stockmodel = _context.Stocks.Find(id);
            if (stockmodel == null)
            {
                return NotFound();
            }

            stockmodel.UpdateFromDto(updateDto);
            _context.SaveChanges();

            return Ok(stockmodel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stockmodel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (stockmodel == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stockmodel);
            _context.SaveChanges();
            return NoContent();


        }
        **/

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            //var stock = _context.Stocks.ToList().Select(s => s.ToStockDto());
            //var stock = _context.Stocks
            //            .AsNoTracking()
            //            .Select(s => s.ToStockDto())
            //            .ToList();
            //var stock = await _context.Stocks.ToListAsync();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetAllAsync(query);

            var stockdto = stock.Select(s => s.ToStockDto()).ToList();
            return Ok(stock);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetbyId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var stock = await _context.Stocks.FindAsync(id);
            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockmodel = stockDto.ToStockFromCreateDTO();
            //await _context.Stocks.AddAsync(stockmodel);
            //await _context.SaveChangesAsync();
            await _stockRepo.CreateAsync(stockmodel);
            return CreatedAtAction(nameof(GetbyId), new {id=stockmodel.Id},stockmodel.ToStockDto());

        }
        [HttpPut]
        [Route("{id:int}")]
        //[HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (updateDto == null)
            {
                return BadRequest("Update data is required.");
            }

            //var stockmodel = await _context.Stocks.FindAsync(id);
            var stockmodel = await _stockRepo.UpdateAsync(id, updateDto);
            if (stockmodel == null)
            {
                return NotFound();
            }

            //stockmodel.UpdateFromDto(updateDto);
            //await _context.SaveChangesAsync();

            return Ok(stockmodel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var stockmodel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            var stockmodel = await _stockRepo.DeleteAsync(id);
            if (stockmodel == null)
            {
                return NotFound();
            }
            //_context.Stocks.Remove(stockmodel);
            //await _context.SaveChangesAsync();
            return NoContent();


        }
    }
}
