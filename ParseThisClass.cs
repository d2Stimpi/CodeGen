using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stimpi
{
    public class SampleClass : Entity, BaseObject
    {
        private int _number = 10;
        private static float _floatNum;
        public QuadComponent _quad;
        public CameraComponent _camera;

        public int Number { get => _number + 5; }
        public int AnotherNumber { get => _number + 15; set => _number = value; }
        public CameraComponent Camera { get => _camera; }

        public void MethodOne()
        {
            Console.WriteLine("Some printed text");
            Console.WriteLine($"Result of MethodTwo() = {MethodTwo(10, MethodTwo(3, 3))}");
            {
                Console.WriteLine("Text in a block");
            }
        }

        private static int MethodTwo(int arg1, int arg2)
        {
            return arg1 + arg2;
        }

        private static int MethodThree(BaseObject arg1, BaseObject arg2)
        {
            return arg1 + arg2;
        }

        public T GetComponent<T>() where T : Component, new()
        {
            if (!HasComponent<T>())
                return null;

            T component = new T() { Entity = this };
            return component;
        }
    }

    private static class HelperClass
    {
        void HelperMethod()
        {

        }
    }
}