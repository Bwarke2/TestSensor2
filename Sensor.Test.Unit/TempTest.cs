using NUnit.Framework;
using System;
using System.IO;

namespace Sensor.Test.Unit
{
    public class TempSensorTests
    {
        private TempSensor.TempSensor _uut;
        private StringWriter _str;

        [SetUp]
        public void Setup()
        {
            _str = new StringWriter();   //Setup string writer
            Console.SetOut(_str);        //Set console output to str

            _uut = new TempSensor.TempSensor();
        }

        [Test]
        public void StartTempIsZero()
        {
            Assert.That(_uut._curretTemp, Is.EqualTo(0));
        }

        [TestCase(-272.9)]
        [TestCase(300)]
        public void UpdateUpdatesTempWhenAboveAbsZero(double inTemp)
        {
            _uut.Update(inTemp);
            Assert.That(_uut._curretTemp, Is.EqualTo(inTemp));
        }

        [TestCase(-273.0)]
        [TestCase(-300)]
        public void UpdateDoesNotUpdatesTempWhenBelowAbsZero(double inTemp)
        {
            double lastTemp = _uut._curretTemp;
            _uut.Update(inTemp);
            Assert.That(_uut._curretTemp, Is.EqualTo(lastTemp));
        }

        [TestCase(-273.0)]
        [TestCase(-300)]
        public void UpdateWritesToTerminalWhenBelowAbsZero(double inTemp)
        {
            _uut.Update(inTemp);
            Assert.That(_str.ToString().Contains("Error below abs. zero"));
        }
        [TestCase(-272.9)]
        [TestCase(300)]
        public void UpdateDoesNotWriteToTerminalWhenAboveAbsZero(double inTemp)
        {
            _uut.Update(inTemp);
            Assert.That(!_str.ToString().Contains("Error below abs. zero"));
        }
    }
}