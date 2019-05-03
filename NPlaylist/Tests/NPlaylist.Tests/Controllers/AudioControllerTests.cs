using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPlaylist.Models.Audio;
using NPlaylist.Persistence.DbModels;
using NPlaylist.Persistence.Tests.EntityBuilders;
using NPlaylist.Services.AudioService;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NPlaylist.Tests.Controllers
{
    public class AudioControllerTests
    {
		[Fact]
        public async Task IndexReturnsViewResult_ExpectedNotNull()
        {
            var service = Substitute.For<IAudioService>();

            var controller = new AudioControllerBuilder().WithAudioService(service)
                .Build();
            
            var result = await controller.Index(CancellationToken.None) as ViewResult;
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task EditGet_NoAudios_ReturnsNotFoundHttpCode()
        {
            var audioService = Substitute.For<IAudioService>();
            audioService
                .GetAudioAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult((Audio)null));

            var sut = new AudioControllerBuilder()
                .WithAudioService(audioService)
                .Build();
            var sutView = await sut.Edit(Arg.Any<Guid>(), CancellationToken.None) as ViewResult;

            var actual = sutView.StatusCode;
            actual.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task EditGet_Forbidden_ReturnsForbiddenHttpCode()
        {
            var audio = new AudioBuilder().Build();

            var audioService = Substitute.For<IAudioService>();
            audioService
                .GetAudioAsync(audio.AudioId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(audio));

            var authService = Substitute.For<IAuthorizationService>();
            authService
                .AuthorizeAsync(Arg.Any<ClaimsPrincipal>(), audio, Arg.Any<IAuthorizationRequirement[]>())
                .Returns(Task.FromResult(AuthorizationResult.Failed()));

            var sut = new AudioControllerBuilder()
                .WithAudioService(audioService)
                .WithAuthService(authService)
                .Build();

            var actual = await sut.Edit(audio.AudioId, CancellationToken.None);
            actual.Should().BeOfType<ForbidResult>();
        }

        [Fact]
        public async Task EditGet_ExistingAudio_ReturnsViewWithModelContainingExpectedAudioId()
        {
            var audio = new AudioBuilder().Build();

            var audioService = Substitute.For<IAudioService>();
            audioService
                .GetAudioAsync(audio.AudioId, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(audio));

            var authService = Substitute.For<IAuthorizationService>();
            authService
                .AuthorizeAsync(Arg.Any<ClaimsPrincipal>(), audio, Arg.Any<IAuthorizationRequirement[]>())
                .Returns(Task.FromResult(AuthorizationResult.Success()));

            var sut = new AudioControllerBuilder()
                .WithAudioService(audioService)
                .WithAuthService(authService)
                .WithMapper(new MapperBuilder().WithDefaultProfile().Build())
                .Build();
            var sutView = await sut.Edit(audio.AudioId, CancellationToken.None) as ViewResult;

            var actual = (sutView.Model as AudioViewModel).AudioId;
            actual.Should().Be(audio.AudioId);
        }

        [Fact]
        public async Task EditPost_Forbidden_ReturnsForbiddenHttpCode()
        {
            var authService = Substitute.For<IAuthorizationService>();
            authService
                .AuthorizeAsync(Arg.Any<ClaimsPrincipal>(), Arg.Any<Audio>(), Arg.Any<IAuthorizationRequirement[]>())
                .Returns(Task.FromResult(AuthorizationResult.Failed()));

            var sut = new AudioControllerBuilder()
                .WithAuthService(authService)
                .WithMapper(new MapperBuilder().WithDefaultProfile().Build())
                .Build();

            var actual = await sut.Edit(new AudioViewModel(), CancellationToken.None);
            actual.Should().BeOfType<ForbidResult>();
        }

        [Fact]
        public async Task EditPost_NoAudios_ReturnsNotFoundHttpCode()
        {
            var authService = Substitute.For<IAuthorizationService>();
            authService
                .AuthorizeAsync(Arg.Any<ClaimsPrincipal>(), Arg.Any<Audio>(), Arg.Any<IAuthorizationRequirement[]>())
                .Returns(Task.FromResult(AuthorizationResult.Success()));

            var audioService = Substitute.For<IAudioService>();
            audioService
                .When(x => x.UpdateAudioAsync(Arg.Any<Audio>(), Arg.Any<CancellationToken>()))
                .Do(x => throw new KeyNotFoundException());

            var sut = new AudioControllerBuilder()
                .WithAuthService(authService)
                .WithAudioService(audioService)
                .WithMapper(new MapperBuilder().WithDefaultProfile().Build())
                .Build();
            var sutView = await sut.Edit(new AudioViewModel(), CancellationToken.None) as ViewResult;

            var actual = sutView.StatusCode;
            actual.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}