﻿using System;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System.Threading;

namespace CanoePoloLeagueOrganiserTests
{
    // These tests are probaly my least favourite, I think they expose a design flaw, but I haven't been able to think of anything better.
    // The main thing that bothers me is the amount of framework / context that the tests require.
    // I think the problem is that the bool return value, the message and the pragmatisation level are all return values of the AcceptableSolution method really, so maybe I should refactor towards that.
    public class PragmatisePermutationsTests
    {
        [Fact]
        public void DontCheckTooOften()
        {
            var permutations = CreatePermuations(1000);

            var timeStart = DateTime.Now;

            new PragmatisePermutations(
                permutations, 
                new CheckingPragmatisationTakes100Milliseconds(), 
                new AnyRunningOptimalGameOrder())
            .PragmatisedPermutations()
            .ToList();

            var timeTaken = DateTime.Now.Subtract(timeStart);

            Assert.True(timeTaken < TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void AllowPragmatasingOnTimeTaken()
        {
            var permutations = CreatePermuations(100000);

            var timeStart = DateTime.Now;

            new PragmatisePermutations(
                permutations,
                new PragmatiseAfter100Milliseconds(),
                new AnyRunningOptimalGameOrder())
            .PragmatisedPermutations()
            .ToList();

            var timeTaken = DateTime.Now.Subtract(timeStart);

            Assert.True(timeTaken < TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void AllowPragmatasingOnConsecutiveGames()
        {
            var permutations = CreatePermuations(10000);

            var timeStart = DateTime.Now;

            new PragmatisePermutations(
                permutations,
                new PragmatiseWhenZeroConsecutiveGames(),
                new ZeroConsecutiveGames())
            .PragmatisedPermutations()
            .ToList();

            var timeTaken = DateTime.Now.Subtract(timeStart);

            Assert.True(timeTaken < TimeSpan.FromSeconds(1));
        }

        IEnumerable<Game[]> CreatePermuations(int numberOfPermutations)
        {
            for (int i = 0; i < numberOfPermutations; i++)
                yield return new Game[] { };
        }
    }

    internal class AnyRunningOptimalGameOrder : IRunningOptimalGameOrder
    {
        public uint RunningOccurencesOfTeamsPlayingConsecutiveGames => 100;
    }

    internal class CheckingPragmatisationTakes100Milliseconds : IPragmatiser
    {
        public string Message => throw new NotImplementedException();

        public PragmatisationLevel Level => throw new NotImplementedException();

        public bool AcceptableSolution(TimeSpan timeElapsed, uint lowestOccurencesOfTeamsPlayingConsecutiveGames)
        {
            Thread.Sleep(100);
            return false;
        }
    }

    internal class PragmatiseAfter100Milliseconds : IPragmatiser
    {
        public string Message => throw new NotImplementedException();

        public PragmatisationLevel Level => throw new NotImplementedException();

        public bool AcceptableSolution(TimeSpan timeElapsed, uint lowestOccurencesOfTeamsPlayingConsecutiveGames)
        {
            Thread.Sleep(100);

            return timeElapsed.TotalMilliseconds > 100;
        }
    }

    internal class ZeroConsecutiveGames : IRunningOptimalGameOrder
    {
        public uint RunningOccurencesOfTeamsPlayingConsecutiveGames => 0;
    }

    internal class PragmatiseWhenZeroConsecutiveGames : IPragmatiser
    {
        public string Message => throw new NotImplementedException();

        public PragmatisationLevel Level => throw new NotImplementedException();

        public bool AcceptableSolution(TimeSpan timeElapsed, uint lowestOccurencesOfTeamsPlayingConsecutiveGames)
        {
            Thread.Sleep(100);

            return lowestOccurencesOfTeamsPlayingConsecutiveGames == 0;
        }
    }
}
