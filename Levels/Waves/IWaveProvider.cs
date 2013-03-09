using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.PrincessDefense.Levels.Waves
{
    public interface IWaveProvider
    {
        /// <summary>
        /// Creates wave instructions for the current wave.
        /// </summary>
        WaveInstruction CreateWave(int waveIndex);
    }
}
