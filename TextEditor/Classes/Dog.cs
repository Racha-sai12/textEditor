namespace TextEditor.Classes
{
    public class Dog
    {
        private string Name;
        private int Age;



        public void SetAge(int age)
        {
            if(age > 30)
            {
                Age = 30;
            }
            else
            {
                Age = age;
            }

        }

    }
}
