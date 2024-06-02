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
        private readonly NotificationService notificationService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ProsecutorAssignationService(UserManager<User> userManager, IUnitOfWork unitOfWork, 
            IMapper mapper, NotificationService notificationService)
        {
            _userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.notificationService = notificationService;
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
            var caseFileExchange = $"caseFile_{casefileId}";
            await notificationService.ConnectToExchange($"user_{randomProsecutor.Id}", caseFileExchange);
            await notificationService.Publish(caseFileExchange, $"Prosecutor {randomProsecutor.UserName} has been assigned to case file ${casefileId}");
            // notify prosecutor about assignation to casefile

        }
    }
}
