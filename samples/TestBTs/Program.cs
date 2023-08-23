﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System;
using LibGameAI.BehaviorTrees;

namespace LibGameAI.Samples.TestBTs
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            ITask getMatches = new LeafTask(
                () => { Console.WriteLine("Get matches"); return TaskResult.Success; });
            ITask getGasoline = new LeafTask(
                () => { Console.WriteLine("Get gasoline"); return TaskResult.Success; });
            CompositeTask ndSeq = new NonDeterministicSequenceTask(
                rnd.Next, getMatches, getGasoline);
            ITask douseDoor = new LeafTask(
                () => { Console.WriteLine("Douse Door"); return TaskResult.Success; });
            ITask igniteDoor = new LeafTask(
                () => { Console.WriteLine("Ignite Door"); return TaskResult.Success; });
            CompositeTask dSeq = new SequenceTask(ndSeq, douseDoor, igniteDoor);
            ITask bargeDoor = new LeafTask(
                () => { if (rnd.NextDouble() < 0.3) {
                    Console.WriteLine("Barge Door"); return TaskResult.Success; } return TaskResult.Failure; });
            CompositeTask ndSel = new NonDeterministicSelectorTask(rnd.Next, bargeDoor, dSeq);
            ITask entering = new LeafTask(
                () => { if (rnd.NextDouble() < 0.3) {
                    Console.WriteLine("Entering..."); return TaskResult.Success; } return TaskResult.Failure; });
            ITask openDoor = new LeafTask(
                () => { if (rnd.NextDouble() < 0.3) {
                    Console.WriteLine("Open door..."); return TaskResult.Success; } return TaskResult.Failure; });
            CompositeTask dSel = new SelectorTask(entering, openDoor, ndSel);
            ITask bt = dSel;

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"\n ===== RUN {i} ===== ");
                bt.Run();
            }

        }
    }

}
