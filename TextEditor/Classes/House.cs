namespace TextEditor.Classes
{
    public class House
    {
        public string Address {  get; set; }
        public Dog Dog { get; set; }


        public void DoSomethingWithTheDog()
        {
            Dog = new Dog();

            Dog streetDog;

            Dog richDog = new Dog();


            Dog.SetAge(50);

        }



    }
}
