/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

namespace LibGameAI.BehaviorTrees
{
    // Inverts the execution result of the decorated task
    public class InverterDecoratorTask : DecoratorTask
    {

        // Constructor, requires the decorated task which it passes on to the
        // base class constructor
        public InverterDecoratorTask(ITask decoratedTask)
            : base(decoratedTask)
        {
        }

        // Run the decorated task and return the opposite execution result
        public override TaskResult Run()
        {
            TaskResult result = DecoratedTask.Run();
            switch (result)
            {
                case TaskResult.Success:
                    return TaskResult.Failure;
                case TaskResult.Failure:
                    return TaskResult.Success;
                default:
                    return TaskResult.Running;
            }
        }
    }
}
