/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System.Collections.Generic;

namespace LibGameAI.BehaviorTrees
{
    /// <summary>
    /// Defines a strategy for ordering child tasks.
    /// </summary>
    public interface ITaskOrderStrategy
    {
        IEnumerable<ITask> GetTasks(IList<ITask> tasks);
    }
}
