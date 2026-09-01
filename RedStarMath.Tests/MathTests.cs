global using NStar.Core;
global using NStar.Core.Tests;
global using NStar.Linq;
global using NStar.Mpir;
global using System;
global using System.Globalization;
global using System.Numerics;
global using static NStar.Core.Extents;
global using static NStar.Core.Tests.Global;
global using static System.Math;
global using E = System.Linq.Enumerable;
global using G = System.Collections.Generic;

namespace RedStarMath.Tests;

[TestClass]
public class MathTests
{
	[TestMethod]
	public void TestAtan2()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 100000; i++)
		{
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r = BitConverter.ToDouble(bytes.AsSpan());
			if (random.Next(1000) != 0)
			{
				bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
				if (random.Next(2) == 0)
					bytes.Resize(8);
				else
					bytes.ResizeLeft(8);
			}
			var r2 = BitConverter.ToDouble(bytes.AsSpan());
			if (r is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue
				|| r2 is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue
				|| Abs(r) < Math.Shift(1d, -52) || Abs(r2) < Math.Shift(1d, -52) || r == 0 && r2 == 0)
			{
#pragma warning disable IDE0079 // Удалить ненужное подавление
#pragma warning disable S127
				i--;
#pragma warning restore S127
#pragma warning restore IDE0079 // Удалить ненужное подавление
				continue;
			}
			var m = r.ToDecimal();
			var m2 = r2.ToDecimal();
			if (!(m.ToReal().Equals(r) && m2.ToReal().Equals(r2)))
			{
#pragma warning disable IDE0079 // Удалить ненужное подавление
#pragma warning disable S127
				i--;
#pragma warning restore S127
#pragma warning restore IDE0079 // Удалить ненужное подавление
				continue;
			}
			var atan = Atan2(r, r2);
			var dAtan = Math.Atan2(m, m2);
			Assert.IsLessThanOrEqualTo(Math.Shift(Max(Abs(atan), 1), -52), Abs(atan - dAtan.ToReal()));
		}
	}

	[TestMethod]
	public void TestHyperbolic()
	{
		Assert.AreEqual(0, decimal.Zero.Sinh());
		Assert.AreEqual(1, decimal.Zero.Cosh());
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 10000; i++)
		{
			while (true)
			{
				bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
				if (random.Next(2) == 0)
					bytes.Resize(8);
				else
					bytes.ResizeLeft(8);
				var r = BitConverter.ToDouble(bytes.AsSpan());
				if (r is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue || Abs(r) < Math.Shift(1d, -52))
					continue;
				var m = r.ToDecimal();
				if (!m.ToReal().Equals(r))
					continue;
				var sinh = Sinh(r);
				if (!(sinh is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| Abs(r) < Math.Shift(1d, -52)))
					Assert.IsLessThanOrEqualTo(Max(sinh.Abs(), 1).Shift(-50), Abs(sinh - m.Sinh().ToReal()));
				var cosh = Cosh(r);
				if (!(cosh is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| Abs(r) < Math.Shift(1d, -52)))
					Assert.IsLessThanOrEqualTo(Max(cosh.Abs(), 1).Shift(-50), Abs(cosh - m.Cosh().ToReal()));
				var tanh = Tanh(r);
				if (!(tanh is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| Abs(r) < Math.Shift(1d, -52)))
					Assert.IsLessThanOrEqualTo(Math.Shift(1d, -52), Abs(tanh - m.Tanh().ToReal()));
				break;
			}
		}
		for (var i = 0; i < 10000; i++)
		{
			var r = (1 - random.NextDouble()) * PI / 2;
			var m = r.ToDecimal();
			Assert.IsLessThan(m, m.Sin());
			Assert.IsGreaterThan(m, m.Tan());
		}
	}

	[TestMethod]
	public void TestInverseHyperbolic()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		for (var i = 0; i < 5000; i++)
		{
			while (true)
			{
				var r = random.NextDouble();
				if (r is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue || Abs(r) < Math.Shift(1d, -52))
					continue;
				var m = r.ToDecimal();
				var sinh = m / (1 - m);
				var sinh2 = sinh.Asinh().Sinh();
				Assert.IsLessThanOrEqualTo(Max(sinh, 1m).ShiftDec(-27), (sinh - sinh2).Abs());
				var cosh = m.Reciproc();
				var cosh2 = cosh.Acosh().Cosh();
				Assert.IsLessThanOrEqualTo(Max(cosh, 1m).ShiftDec(-27), (cosh - cosh2).Abs());
				var tanh = m * 2 - 1;
				var tanh2 = tanh.Atanh().Tanh();
				Assert.IsLessThanOrEqualTo(Max(tanh, 1m).ShiftDec(-27), (tanh - tanh2).Abs());
				break;
			}
		}
	}

	[TestMethod]
	public void TestInverseTrigonometry()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		for (var i = 0; i < 5000; i++)
		{
			while (true)
			{
				var r = random.NextDouble() * (random.Next(2) == 0 ? -1 : 1);
				if (r is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue || Abs(r) < Math.Shift(1d, -52))
					continue;
				var m = r.ToDecimal();
				var m2 = m.Asin().Sin();
				var m3 = m.Acos().Cos();
				var m4 = m.Atan().Tan();
				Assert.IsLessThanOrEqualTo(1e-28m, (m - m2).Abs());
				Assert.IsLessThanOrEqualTo(1e-28m, (m - m3).Abs());
				Assert.IsLessThanOrEqualTo(1e-28m, (m - m4).Abs());
				break;
			}
		}
	}

	[TestMethod]
	public void TestLog()
	{
		Assert.AreEqual(LongDecimal.NegativeInfinity, LongDecimal.Zero.Log());
		Assert.AreEqual(LongDecimal.PositiveInfinity, LongDecimal.PositiveInfinity.Log());
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NegativeInfinity.Log()));
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NaN.Log()));
		Assert.AreEqual(LongDecimal.Zero, LongDecimal.One.Log());
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 10000; i++)
		{
			while (true)
			{
				bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
				if (random.Next(2) == 0)
					bytes.Resize(8);
				else
					bytes.ResizeLeft(8);
				var r = Abs(BitConverter.ToDouble(bytes.AsSpan()));
				if (r is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue || Abs(r) < Math.Shift(1d, -52))
					continue;
				var m = r.ToDecimal();
				var log = m.Log();
				var dLog = Log((double)m);
				Assert.IsLessThanOrEqualTo(Max(Abs(dLog), 1).Shift(-51), Abs(dLog - log.ToReal()));
				break;
			}
		}
	}

	[TestMethod]
	public void TestPower()
	{
		Assert.AreEqual(LongDecimal.NegativeInfinity, LongDecimal.Zero.Log());
		Assert.AreEqual(LongDecimal.PositiveInfinity, LongDecimal.PositiveInfinity.Log());
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NegativeInfinity.Log()));
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NaN.Log()));
		Assert.AreEqual(LongDecimal.Zero, LongDecimal.One.Log());
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 10000; i++)
		{
			while (true)
			{
				bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
				if (random.Next(2) == 0)
					bytes.Resize(8);
				else
					bytes.ResizeLeft(8);
				var r = Abs(BitConverter.ToDouble(bytes.AsSpan()));
				if (r is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue || Abs(r) < Math.Shift(1d, -52))
					continue;
				var m = r.ToDecimal();
				var dn = m.Log().Exp();
				Assert.IsLessThanOrEqualTo(Max(m, 1m).ShiftDec(-27), (m - dn).Abs());
				var m2 = 2m.Power(m.Log2());
				Assert.IsLessThanOrEqualTo(Max(m, 1m).ShiftDec(-26), (m - m2).Abs());
				var d10 = 10m.Power(m.Log10());
				Assert.IsLessThanOrEqualTo(Max(m, 1m).ShiftDec(-26), (m - d10).Abs());
				var m3 = 3m.Power(m.Log(3));
				Assert.IsLessThanOrEqualTo(Max(m, 1m).ShiftDec(-27), (m - m3).Abs());
				break;
			}
		}
	}

	[TestMethod]
	public void TestShifts()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 1000000; i++)
		{
			while (true)
			{
				bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
				if (random.Next(2) == 0)
					bytes.Resize(8);
				else
					bytes.ResizeLeft(8);
				var r = BitConverter.ToDouble(bytes.AsSpan());
				if (r is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue || Abs(r) < Math.Shift(1d, -52))
					continue;
				var shiftAmount = random.Next(97) - 48;
				var shifted = r.Shift(shiftAmount);
				if (shifted is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| Abs(shifted) < Math.Shift(1d, -52))
					continue;
				var m = r.ToDecimal();
				if (!m.ToReal().Equals(r))
					continue;
				var dShifted = m.Shift(shiftAmount);
				var epsilon = Math.Shift(Max(Abs(shifted), 1), -52);
				Assert.IsLessThanOrEqualTo(epsilon, Abs(shifted - dShifted.ToReal()));
				break;
			}
		}
	}

	[TestMethod]
	public void TestSqrt()
	{
		Assert.AreEqual(decimal.Zero, decimal.Zero.Sqrt());
		Assert.AreEqual(decimal.One, decimal.One.Sqrt());
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 1000000; i++)
		{
			while (true)
			{
				bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
				if (random.Next(2) == 0)
					bytes.Resize(8);
				else
					bytes.ResizeLeft(8);
				var r = BitConverter.ToDouble(bytes.AsSpan());
				if (r is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue || Abs(r) < Math.Shift(1d, -52))
					continue;
				var sqrt = r.Sqrt();
				if (sqrt is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| Abs(sqrt) < Math.Shift(1d, -52))
					continue;
				var m = r.ToDecimal();
				if (!m.ToReal().Equals(r))
					continue;
				var dSqrt = m.Sqrt();
				var epsilon = Math.Shift(Max(Abs(sqrt), 1), -52);
				Assert.IsLessThanOrEqualTo(epsilon, Abs(sqrt - dSqrt.ToReal()));
				break;
			}
		}
	}

	[TestMethod]
	public void TestToDecimal()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 1000000; i++)
		{
			while (true)
			{
				bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
				if (random.Next(2) == 0)
					bytes.Resize(8);
				else
					bytes.ResizeLeft(8);
				var r = BitConverter.ToDouble(bytes.AsSpan());
				if (r is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue || Abs(r) < Math.Shift(1d, -50))
					continue;
				var m = r.ToDecimal();
				Assert.AreEqual(r.ToString("F28").Take(29).ToString(x => x), m.ToString("F28").Take(29).ToString(x => x));
				break;
			}
		}
	}

	[TestMethod]
	public void TestToDouble()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 1000000; i++)
		{
			while (true)
			{
				bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
				if (random.Next(2) == 0)
					bytes.Resize(8);
				else
					bytes.ResizeLeft(8);
				var r = BitConverter.ToDouble(bytes.AsSpan());
				if (r is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue || Abs(r) < Math.Shift(1d, -50))
					continue;
				var m = r.ToDecimal();
				Assert.IsLessThanOrEqualTo(Math.Shift(1d, -64), r - m.ToReal());
				break;
			}
		}
	}

	[TestMethod]
	public void TestTrigonometry()
	{
		Assert.AreEqual(0, decimal.Zero.Sin());
		Assert.AreEqual(1, (Math.DecimalPi / 2).Sin(), 1e-27m);
		Assert.AreEqual(0, Math.DecimalPi.Sin(), 1e-27m);
		Assert.AreEqual(-1, (Math.DecimalTau - Math.DecimalPi / 2).Sin(), 1e-27m);
		Assert.AreEqual(0, (Math.DecimalPi * 2).Sin(), 1e-27m);
		Assert.AreEqual(-1, (-Math.DecimalPi / 2).Sin(), 1e-27m);
		Assert.AreEqual(0, (-Math.DecimalPi).Sin(), 1e-27m);
		Assert.AreEqual(1, (-3 * Math.DecimalPi / 2).Sin(), 1e-27m);
		Assert.AreEqual(0, (-Math.DecimalPi * 2).Sin(), 1e-27m);
		Assert.AreEqual(1, decimal.Zero.Cos());
		Assert.AreEqual(0, (Math.DecimalPi / 2).Cos(), 1e-27m);
		Assert.AreEqual(-1, Math.DecimalPi.Cos(), 1e-27m);
		Assert.AreEqual(0, (3 * Math.DecimalPi / 2).Cos(), 1e-27m);
		Assert.AreEqual(1, (Math.DecimalPi * 2).Cos(), 1e-27m);
		Assert.AreEqual(0, (-Math.DecimalPi / 2).Cos(), 1e-27m);
		Assert.AreEqual(-1, (-Math.DecimalPi).Cos(), 1e-27m);
		Assert.AreEqual(0, (-3 * Math.DecimalPi / 2).Cos(), 1e-27m);
		Assert.AreEqual(1, (-Math.DecimalPi * 2).Cos(), 1e-27m);
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		for (var i = 0; i < 10000; i++)
		{
			while (true)
			{
				var r = Pow(2, random.NextDouble() * 128 - 64);
				if (r is double.NaN or < (double)decimal.MinValue or > (double)decimal.MaxValue || Abs(r) < Math.Shift(1d, -52))
					continue;
				var m = r.ToDecimal();
				if (!m.ToReal().Equals(r))
					continue;
				Assert.IsLessThanOrEqualTo(Math.Shift(1d, -52), Abs(Sin(r) - m.Sin().ToReal()));
				Assert.IsLessThanOrEqualTo(Math.Shift(1d, -52), Abs(Cos(r) - m.Cos().ToReal()));
				Assert.IsLessThanOrEqualTo(Max(Tan(r).Abs().Power(1.5), 1).Shift(-52), Abs(Tan(r) - m.Tan().ToReal()));
				break;
			}
		}
		for (var i = 0; i < 10000; i++)
		{
			var r = (1 - random.NextDouble()) * PI / 2;
			var m = r.ToDecimal();
			Assert.IsLessThan(m, m.Sin());
			Assert.IsGreaterThan(m, m.Tan());
		}
	}
}
