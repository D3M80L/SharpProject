#define SOLUTION_O
//#define SOLUTION_A
//#define SOLUTION_B

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE011
{
    

    class Program
    {
        static void Main(string[] args)
        {
            var codeGenerator = new CodeGenerator();

            ThreadPool.QueueUserWorkItem(_ => codeGenerator.Generate());
            ThreadPool.QueueUserWorkItem(_ => codeGenerator.Generate());
            Thread.Sleep(1000);
        }
    }

    class CodeGenerator
    {

        #region Original
#if SOLUTION_O
        /// <summary>
        /// Please note, that this implementation is not thread save.\
        /// The random class has some private fields which are updated after each call of Next()
        /// 
        /// Solution: An easy solution would be to wrap the call of Next() in a using block. 
        /// In this case the performance is not good, because a lock is happenning which may slow down the application.
        /// </summary>
        private static Random _rnd = new Random();
#endif
        #endregion

        #region Solution A
#if SOLUTION_A

        /// <summary>
        /// You can mark your static field with the attibute ThreadStatic.
        /// Each thread store its own value and instance in this field.
        /// 
        /// IMPORTANT: Please note, that the example below only assigns the field with the new instance only in the main thread.
        /// Each thread needs to call and create the instance separately.
        /// </summary>
        [ThreadStatic]
        private static Random _rnd = new Random();

#endif
        #endregion

        #region Solution B

        /// <summary>
        /// Each thread will have its own instance of thread. 
        /// This is wrapped in the ThreadLocal class.
        /// 
        /// NOTE: please take a look, at the seed which is passed as an argument in the Random class.
        /// This is because two threads, when would create an instance of Random during a very short period of time, then they could 
        /// generate same values. That's because the Random by default uses time as the seed.
        /// </summary>
        private readonly ThreadLocal<Random> _rndLocal = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));

        #endregion
        //private Random _rnd = new Random(); // NOTE: Random is not thread safe
        //private ThreadLocal<Random> _rnd = new ThreadLocal<Random>(() => new Random());

        /// <summary>
        /// this.GetHashCode() - shows that the same class instance is called
        /// _rnd.GetHashCode() and _rndLocal.Value.GetHashCode() - informs about which instance of Random is called
        /// _rndLocal.GetHashCode() - informs which instance of ThreadLocal is called
        /// </summary>
        public void Generate()
        {

#if !SOLUTION_B
            int i = _rnd.Next();
            Console.WriteLine("{2} = this.GetHashCode()={0}, _rnd.GetHashCode()={1}", this.GetHashCode(), _rnd.GetHashCode(), i);
#else
            int i = _rndLocal.Value.Next();
            Console.WriteLine("{2} = this.GetHashCode()={0}, _rndLocal.GetHashCode()={1} _rndLocal.Value.GetHashCode()={3}", this.GetHashCode(), _rndLocal.GetHashCode(), i, _rndLocal.Value.GetHashCode());
#endif
        }
    }
}
