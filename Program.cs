using System;

namespace DelegateUsageRitmo2k
{
    //public delegate IntPtr MyDelegate(string foo, string bar);
    public delegate IntPtr MyDelegate(string foo, string bar, string baz);

    class Program
    {
        static void Main(string[] args)
        {
            MyDelegate theDel;
            theDel = new MyDelegate(DoSomething);
            Func<string, string, string, IntPtr> myGenDel = new Func<string, string, string, IntPtr>(theDel);

            MyClass myclass = new MyClass(myGenDel);
            myclass.DoSomeMethod("a", "b", "c");

            IntPtr result = myclass.AIntPtr;
            Console.WriteLine(result);

            Console.ReadKey();

        }//end Main

        private static IntPtr DoSomething(string a, string b, string c)
        {
            a = "letter " + a + "\n";
            b = "letter " + b + "\n";
            c = "letter " + c;

            return new IntPtr(a, b, c);
        }
    }//end Program

    public class MyClass
    {
        private Func<string, string, string, IntPtr> MyFieldIsADelegate;
        #region OrigCode
        //private delegate IntPtr MyDelegate();

        //public MyClass(string foo, string bar)
        //{//ctor-0
        //    MyDelegate mydel = () => xxx();
        //    MyDelegate myDel = () => unManagedFuncA(foo, bar);
        //    this.myField = delegate() { return myDel(); };
        //}

        //public MyClass(string foo, string bar, string baz)
        //{//ctor-1
        //    MyDelegate myDel = () => unManagedFuncB(foo, bar, baz);
        //    this.myField = delegate() { return myDel(); };
        //}

        #endregion
        public IntPtr AIntPtr { get; set; }

        public MyClass(Func<string, string, string, IntPtr> mydel)
        {//ctor-2
            this.MyFieldIsADelegate = mydel;
        }

        public void DoSomeMethod(string a, string b, string c)
        {
            //invoke the delegate
            AIntPtr = MyFieldIsADelegate(a, b, c);
        }
    }

    public class IntPtr
    {
        //CTOR
        public IntPtr(string a, string b, string c)
        {
            AnA = a;
            AB = b;
            AC = c;
        }

        public string AnA { get; set; }
        public string AB { get; set; }
        public string AC { get; set; }

        public override string ToString()
        {
            return AnA + AB + AC;
        }

    }//end IntPtr
}
