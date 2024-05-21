using AngularApp1.Server.Models;
using AutoMapper;
using BLL.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using PoliceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProsecutorAssignationService 
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ProsecutorAssignationService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AssignProsecutor(int casefileId)
        {
            var caseFile = await unitOfWork.CaseFileRepository.GetByIdAsync(casefileId);
            ArgumentNullException.ThrowIfNull(caseFile);
            var availableProsecutors = await _userManager.GetUsersInRoleAsync("Prosecutor");
            var random = new Random();
            var randomIndex = random.Next(availableProsecutors.Count);
            var randomProsecutor = availableProsecutors[randomIndex];
            caseFile.ProsecutorId = randomProsecutor.Id;
            await unitOfWork.CaseFileRepository.Update(caseFile);

            // notify prosecutor about assignation to casefile
        }
    }
}
