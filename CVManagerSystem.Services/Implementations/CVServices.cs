using Azure;
using CVManagerSystem.Core.Base;
using CVManagerSystem.Data.DataContext.DbIdentity;
using CVManagerSystem.Data;
using CVManagerSystem.Data.DataContext.DbModels;
using CVManagerSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Mapster;
using CVManagerSystem.Core.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CVManagerSystem.Services.Implementations
{
    public class CVServices : ICVServices
    {

        private readonly AppDataContext _context;
        private readonly IResponseDto _response;
        private readonly ILogger<CVServices> _logger;
        public CVServices(AppDataContext context, IResponseDto response, ILogger<CVServices> logger)
        {
            _context = context;
            _response = response;
            _logger = logger;
        }
        public async Task<IResponseDto> AddCVAsync(CVDto options)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var cv = options.Adapt<CV>();
                _response.IsSuccessed = true;
                var personalInformationId = await AddPersonalInformationAsync(options);
                var experienceInformationId = await AddExperienceInformationAsync(options);
                cv.PersonalInformationId = personalInformationId;
                cv.ExperienceInformationId = experienceInformationId;
                await _context.CV.AddAsync(cv);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Failed to add CV to the database.");
                _response.IsSuccessed = false;
                _response.Errors.Add($"Error: {ex.Message}");
                return _response;
            }
            return _response;
        }

        private async Task<int> AddPersonalInformationAsync(CVDto options)
        {
            try
            {
                var personalInformation = options.Adapt<PersonalInformation>();
                await _context.PersonalInformation.AddAsync(personalInformation);
                await _context.SaveChangesAsync();

                _response.IsSuccessed = true;
                return personalInformation.ID;
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                return -1;
            }
        }

        private async Task<int> AddExperienceInformationAsync(CVDto options)
        {
            try
            {
                var experienceInformation = options.Adapt<ExperienceInformation>();
                _response.IsSuccessed = true;
                await _context.ExperienceInformation.AddAsync(experienceInformation);
                await _context.SaveChangesAsync();


                _response.IsSuccessed = true;
                return experienceInformation.ID;
            }
            catch (Exception ex)
            {
                _response.IsSuccessed = false;
                return -1;
            }
        }

        public async Task<CV> GetCVByIdAsync(int Id)
        {
            var getOneCv = await _context.CV.Include(c => c.PersonalInformation).Include(e => e.ExperienceInformation).AsNoTracking().FirstOrDefaultAsync(c => c.ID == Id);
            return getOneCv;
        }

        public async Task<List<CV>> GetCVsListAsync()
        {
            var allCvs = await _context.CV.Include(c => c.PersonalInformation).Include(e => e.ExperienceInformation).AsNoTracking().ToListAsync();
            return allCvs;
        }

        public async Task<IResponseDto> EditCVAsync(CVDto options, int Id)
        {
            try
            {
                var cv = await _context.CV.FindAsync(Id);
                if (cv == null)
                {
                    _response.IsSuccessed = false;
                    _response.Message = "CV not found.";
                    return _response;
                }

                if (options is not null)
                {
                    await EditExperienceInformationAsync(options, cv.ExperienceInformationId);
                    await EditPersonalInformationAsync(options, cv.PersonalInformationId);
                    cv.Name = options.Name;
                    _context.Entry(cv).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    _response.IsSuccessed = true;
                }
                else
                {
                    _response.IsSuccessed = false;
                    _response.Message = "Invalid object provided for editing.";
                    return _response;
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to edit CV in the database.");
                _response.IsSuccessed = false;
                _response.Errors.Add($"Error: {ex.Message}");
            }
            return _response;
        }

        private async Task<bool> EditExperienceInformationAsync(CVDto options, int Id)
        {
            try
            {
                var experienceInformation = await _context.ExperienceInformation.FindAsync(Id);
                if (experienceInformation is not null)
                {
                    experienceInformation.CityName = options.CityName;
                    experienceInformation.CompanyField = options.CompanyField;
                    experienceInformation.CompanyName = options.CompanyName;
                    _context.Entry(experienceInformation).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to edit experience information in the database.");
                return false;
            }

            return false;
        }

        private async Task<bool> EditPersonalInformationAsync(CVDto options, int Id)
        {
            try
            {
                var personalInformation = await _context.PersonalInformation.FindAsync(Id);
                if (personalInformation is not null)
                {
                    personalInformation.CityName = options.CityName;
                    personalInformation.Email = options.Email;
                    personalInformation.FullName = options.FullName;
                    personalInformation.MobileNumber = options.MobileNumber;
                    _context.Entry(personalInformation).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to edit experience information in the database.");
                return false;
            }

            return false;
        }

        public async Task<IResponseDto> DelelteCVAsync(int Id)
        {
            var cv = await _context.CV.FirstOrDefaultAsync(c => c.ID == Id);
            try
            {
                if(cv is not null)
                {
                    _context.CV.Remove(cv);
                    await _context.SaveChangesAsync();
                    _response.IsSuccessed = true;
                    _response.Message = $"Deleted Succefull";
                }

            }catch (DbUpdateException ex)
            {
                _response.IsSuccessed = false;
                _response.Errors.Add($"Error: {ex.Message}");
                _logger.LogError(ex, "Failed to delete CV from the database.");
            }

            return _response;
        }

        public async Task<List<CV>> GetCVsFilterByCityAsync(string cityName)
        {
            try
            {
                var cvs = await _context.CV
                    .Where(cv => cv.PersonalInformation.CityName == cityName)
                    .ToListAsync();

                return cvs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving CVs by city.");
                throw;
            }
        }
    }
}
