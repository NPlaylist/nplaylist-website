using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using NPlaylist.Models;
using NPlaylist.Persistence.AudioEntries;

namespace NPlaylist.Services.AudioService
{
	public class AudioServiceImpl : IAudioService
	{
		private readonly IMapper _mapper;
		private readonly IAudioEntriesRepository _audioRepository;

		public AudioServiceImpl(
			IMapper mapper,
			IAudioEntriesRepository audioRepository)
		{
			_mapper = mapper;
			_audioRepository = audioRepository;
		}

		public async Task<IEnumerable<AudioEntryViewModel>> GetAudioEntriesAsync(CancellationToken ct)
		{
			var entriesRepo = await _audioRepository.GetAllAsync(ct);
			return _mapper.Map<List<AudioEntryViewModel>>(entriesRepo);
		}
	}
}
