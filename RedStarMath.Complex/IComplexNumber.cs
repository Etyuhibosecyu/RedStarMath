using McNeight;
using System.Runtime.CompilerServices;

namespace RedStarMath;

internal interface IComplexNumber<T, TSelf> : INumber<TSelf>
	where T : struct, IFloatingPoint<T> where TSelf : struct, IComplexNumber<T, TSelf>
{
	protected static abstract Func<T, T, TSelf> Creator { get; }
	public T Imaginary { get; }
	public static TSelf ImaginaryOne { get; } = TSelf.Creator(T.Zero, T.One);
	public T Magnitude => AbsInterface((TSelf)this);
	public static TSelf OneInterface { get; } = TSelf.Creator(T.One, T.Zero);
	public T Phase => Atan2(Imaginary, Real);

	private static T PositiveInfinity => T.Zero switch
	{
		double => T.CreateTruncating(double.PositiveInfinity),
		decimal => throw new OverflowException("Тип decimal не поддерживает бесконечность."),
		LongReal => T.CreateTruncating(LongReal.PositiveInfinity),
		LongDecimal => T.CreateTruncating(LongDecimal.PositiveInfinity),
		_ => throw new InvalidCastException("Поддерживаются типы double, decimal, "
			+ nameof(LongReal) + " и " + nameof(LongDecimal) + '.'),
	};

	public T Real { get; }

	private static object? SinTableObj { get; set; }
	public static TSelf ZeroInterface { get; } = TSelf.Creator(T.Zero, T.Zero);

	private static T Abs(T value) => value switch
	{
		double r => T.CreateTruncating(Math.Abs(r)),
		decimal d => T.CreateTruncating(Math.Abs(d)),
		LongReal lr => T.CreateTruncating(LongReal.Abs(lr)),
		LongDecimal ld => T.CreateTruncating(LongDecimal.Abs(ld)),
		_ => throw new InvalidCastException("Поддерживаются типы double, decimal, "
			+ nameof(LongReal) + " и " + nameof(LongDecimal) + '.'),
	};

	public static T AbsInterface(TSelf value)
	{
		if (T.IsInfinity(value.Real) || T.IsInfinity(value.Imaginary))
			return PositiveInfinity;
		// |value| == sqrt(a^2 + b^2)
		// sqrt(a^2 + b^2) == a/a * sqrt(a^2 + b^2) = a * sqrt(a^2/a^2 + b^2/a^2)
		// Using the above we can factor out the square of the larger component to dodge overflow.
		var c = T.Abs(value.Real);
		var d = T.Abs(value.Imaginary);
		if (c > d)
		{
			var r = d / c;
			return c * Sqrt(T.One + r * r);
		}
		else if (d == T.Zero)
			return c;
		else
		{
			var r = c / d;
			return d * Sqrt(T.One + r * r);
		}
	}

	public static TSelf AcosInterface(TSelf value) =>
		-ImaginaryOne * LogInterface(value + ImaginaryOne * SqrtInterface(OneInterface - value * value));
	public static TSelf AcoshInterface(TSelf value) =>
		LogInterface(value + SqrtInterface(SquareInterface(value) - TSelf.One));

	public static TSelf AsinInterface(TSelf value) =>
		-ImaginaryOne * LogInterface(ImaginaryOne * value + SqrtInterface(OneInterface - value * value));
	public static TSelf AsinhInterface(TSelf value) => LogInterface(value + SqrtInterface(SquareInterface(value) + TSelf.One));

	public static TSelf AtanInterface(TSelf value)
	{
		var Two = TSelf.Creator(T.One + T.One, T.Zero);
		return ImaginaryOne / Two * (LogInterface(OneInterface - ImaginaryOne * value)
			- LogInterface(OneInterface + ImaginaryOne * value));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static decimal Atan2(decimal y, decimal x)
	{
		if (x == 0m && y == 0m)
			return 0m;
		if (x == 0m)
		{
			if (y > 0m)
				return 1.5707963267948966192313216916m;
			return -1.5707963267948966192313216916m;
		}
		if (y == 0m)
		{
			if (x <= 0m)
				return 3.1415926535897932384626433833m;
			return 0m;
		}
		if (MathM.Abs(x) < 1 && y > MathM.Abs(x) * decimal.MaxValue)
			return 1.5707963267948966192313216916m;
		if (MathM.Abs(x) < 1 && y < MathM.Abs(x) * decimal.MinValue)
			return -1.5707963267948966192313216916m;
		var num = MathM.Atan(y / x);
		if (x > 0m)
			return num;
		if (y <= 0m)
			return num - 3.1415926535897932384626433833m;
		return num + 3.1415926535897932384626433833m;
	}

	private static T Atan2(T yValue, T xValue) => (yValue, xValue) switch
	{
		(double yValue2, double xValue2) => T.CreateTruncating(Math.Atan2(yValue2, xValue2)),
		(decimal yValue2, decimal xValue2) => T.CreateTruncating(Atan2(yValue2, xValue2)),
		(LongReal yValue2, LongReal xValue2) => T.CreateTruncating(LongReal.Atan2(yValue2, xValue2)),
		(LongDecimal yValue2, LongDecimal xValue2) => T.CreateTruncating(LongDecimal.Atan2(yValue2, xValue2)),
		_ => throw new InvalidCastException("Поддерживаются типы double, decimal, "
			+ nameof(LongReal) + " и " + nameof(LongDecimal) + '.'),
	};

	public static TSelf AtanhInterface(TSelf value) =>
		LogInterface((TSelf.One + value) / (TSelf.One - value)) / (TSelf.One + TSelf.One);

	public static TSelf ConjugateInterface(TSelf value) => TSelf.Creator(value.Real, -value.Imaginary);

	private static T Cos(T value) => value switch
	{
		double r => T.CreateTruncating(Math.Cos(r)),
		decimal d => T.CreateTruncating(MathM.Cos(d)),
		LongReal lr => T.CreateTruncating(LongReal.Cos(lr)),
		LongDecimal ld => T.CreateTruncating(LongDecimal.Cos(ld)),
		_ => throw new InvalidCastException("Поддерживаются типы double, decimal, "
			+ nameof(LongReal) + " и " + nameof(LongDecimal) + '.'),
	};

	public static TSelf CosInterface(TSelf value)
	{
		var a = value.Real;
		var b = value.Imaginary;
		return TSelf.Creator(Cos(a) * Cosh(b), -(Sin(a) * Sinh(b)));
	}

	private static T Cosh(T value) => value switch
	{
		double r => T.CreateTruncating(Math.Cosh(r)),
		decimal d => T.CreateTruncating(MathM.Cosh(d)),
		LongReal lr => T.CreateTruncating(LongReal.Cosh(lr)),
		LongDecimal ld => T.CreateTruncating(LongDecimal.Cosh(ld)),
		_ => throw new InvalidCastException("Поддерживаются типы double, decimal, "
			+ nameof(LongReal) + " и " + nameof(LongDecimal) + '.'),
	};

	public static TSelf CoshInterface(TSelf value)
	{
		var a = value.Real;
		var b = value.Imaginary;
		return TSelf.Creator(Cosh(a) * Cos(b), Sinh(a) * Sin(b));
	}

	public bool EqualsInterface(object? obj) => obj switch
	{
		null => false,
		TSelf ts => Real.Equals(ts.Real) && Imaginary.Equals(ts.Imaginary),
		T t => Real == t && Imaginary == T.Zero,
		int i => (TSelf)this == TSelf.CreateTruncating(i),
		uint ui => (TSelf)this == TSelf.CreateTruncating(ui),
		long li => (TSelf)this == TSelf.CreateTruncating(li),
		ulong uli => (TSelf)this == TSelf.CreateTruncating(uli),
		double d => (TSelf)this == TSelf.CreateTruncating(d),
		float f => (TSelf)this == TSelf.CreateTruncating(f),
		short si => (TSelf)this == TSelf.CreateTruncating(si),
		ushort usi => (TSelf)this == TSelf.CreateTruncating(usi),
		byte y => (TSelf)this == TSelf.CreateTruncating(y),
		sbyte sy => (TSelf)this == TSelf.CreateTruncating(sy),
		System.Numerics.Complex c => Real.Equals(c.Real) && Imaginary.Equals(c.Imaginary),
		IConvertible ic => ic.Equals(this),
		_ => false
	};

	public bool EqualsInterface(TSelf value) => Real.Equals(value.Real) && Imaginary.Equals(value.Imaginary);

	private static T Exp(T value) => value switch
	{
		double r => T.CreateTruncating(Math.Exp(r)),
		decimal d => T.CreateTruncating(MathM.Exp(d)),
		LongReal lr => T.CreateTruncating(LongReal.Exp(lr)),
		LongDecimal ld => T.CreateTruncating(LongDecimal.Exp(ld)),
		_ => throw new InvalidCastException("Поддерживаются типы double, decimal, "
			+ nameof(LongReal) + " и " + nameof(LongDecimal) + '.'),
	};

	public static TSelf ExpInterface(TSelf value)
	{
		var temp_factor = Exp(value.Real);
		var result_re = temp_factor * Cos(value.Imaginary);
		var result_im = temp_factor * Sin(value.Imaginary);
		return TSelf.Creator(result_re, result_im);
	}

	public static TSelf FromPolarCoordinatesInterface(T magnitude, T phase) =>
		TSelf.Creator(magnitude * Cos(phase), magnitude * Sin(phase));

	public int GetHashCodeInterface()
	{
		var n1 = 99999997;
		var hash_real = Real.GetHashCode() % n1;
		var hash_imaginary = Imaginary.GetHashCode();
		var final_hashcode = hash_real ^ hash_imaginary;
		return final_hashcode;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static decimal Log(decimal value) => (decimal)((LongDecimal)value).Log();
	//{
	//	if (value == 1m)
	//		return 0m;
	//	else if (value == 0m)
	//		throw new OverflowException("Логарифм нуля - минус бесконечность, а decimal ее не поддерживает.");
	//	else if (value < 0m)
	//		throw new ArgumentOutOfRangeException(nameof(value), "Логарифм отрицательного числа не определен.");
	//	var m = 48;
	//	var pow = Pow(2, m);
	//	var exponent = 0;
	//	while (value >= 1m)
	//	{
	//		value /= 10;
	//		exponent++;
	//	}
	//	while (value < 0.1m)
	//	{
	//		value *= 10;
	//		exponent--;
	//	}
	//	var s = value * pow * pow;
	//	var agm = AGM(4 * pow / s, pow) / pow;
	//	return MathM.PI / (2 * agm) - m * 2 * 0.6931471805599453094172321215m
	//		+ 2.302585092994045684017991455m * exponent;
	//	static decimal AGM(decimal x, decimal y)
	//	{
	//		decimal a = x, b = y;
	//		for (var i = 0; i < 60; i++) // фиксированное число итераций: достаточно для decimal
	//			(a, b) = ((a + b) / 2, Sqrt(a * b));
	//		return a;
	//	}
	//}

	private static T Log(T value) => value switch
	{
		double r => T.CreateTruncating(Math.Log(r)),
		decimal d => T.CreateTruncating(Log(d)),
		LongReal lr => T.CreateTruncating(LongReal.Ln(lr)),
		LongDecimal ld => T.CreateTruncating(LongDecimal.Ln(ld)),
		_ => throw new InvalidCastException("Поддерживаются типы double, decimal, "
			+ nameof(LongReal) + " и " + nameof(LongDecimal) + '.'),
	};

	public static TSelf LogInterface(TSelf value) =>
		TSelf.Creator(Log(AbsInterface(value)), Atan2(value.Imaginary, value.Real));
	public static TSelf LogInterface(TSelf value, T baseValue) => LogInterface(value) / Log(baseValue);

	protected static decimal Pow(decimal value, int exponent) => exponent switch
	{
		2 => value * value,
		3 => value * value * value,
		_ => PowInternal(value, exponent),
	};

	private static T Pow(T value, T power) => (value, power) switch
	{
		(double value2, double power2) => T.CreateTruncating(Math.Pow(value2, power2)),
		(decimal value2, decimal power2) => T.CreateTruncating(MathM.Pow(value2, power2)),
		(LongReal value2, LongReal power2) => T.CreateTruncating(LongReal.Power(value2, power2)),
		(LongDecimal value2, LongDecimal power2) => T.CreateTruncating(LongDecimal.Power(value2, power2)),
		_ => throw new InvalidCastException("Поддерживаются типы double, decimal, "
			+ nameof(LongReal) + " и " + nameof(LongDecimal) + '.'),
	};

	public static TSelf PowInterface(TSelf value, int power) =>
		PowInterface(value, TSelf.Creator(T.CreateTruncating(power), T.Zero));
	public static TSelf PowInterface(TSelf value, T power) => PowInterface(value, TSelf.Creator(power, T.Zero));

	public static TSelf PowInterface(TSelf value, TSelf power)
	{
		if (power == ZeroInterface)
			return OneInterface;
		if (value == ZeroInterface)
			return ZeroInterface;
		if (value.Imaginary == T.Zero && power.Imaginary == T.Zero)
			return TSelf.Creator(Pow(value.Real, power.Real), T.Zero);
		var a = value.Real;
		var b = value.Imaginary;
		var c = power.Real;
		var d = power.Imaginary;
		var rho = AbsInterface(value);
		var theta = Atan2(b, a);
		var newRho = c * theta + d * Log(rho);
		var t = Pow(rho, c) * Pow(T.E, -d * theta);
		return TSelf.Creator(t * Cos(newRho), t * Sin(newRho));
	}

	private static decimal PowInternal(decimal @base, int exponent)
	{
		switch (exponent)
		{
			case 0:
			return 1m;
			case 1:
			return @base;
			case 4:
			@base *= @base;
			return @base * @base;
			case 5:
			var square = @base * @base;
			return square * square * @base;
			default:
			if (@base == 1m)
				return @base;
			var negative = false;
			if (exponent < 0)
			{
				negative = true;
				exponent = -exponent;
			}
			if (@base == 0m)
				return negative ? throw new DivideByZeroException() : 0.0m;
			if (@base == 1.0m)
				return @base;
			if (@base == -1.0m)
				return (exponent & 1) == 0 ? 1.0m : -1.0m;
			if (negative)
				@base = 1.0m / @base;
			var result = 1m;
			for (var i = BitsPerInt - int.LeadingZeroCount(exponent); i >= 0; i--)
			{
				result *= result;
				if ((exponent & 1u << i) != 0)
					result *= @base;
			}
			return result;
		}
	}

	public static TSelf ReciprocInterface(TSelf value) =>
		value.Imaginary == T.Zero ? TSelf.Creator(T.One / value.Real, T.Zero) : OneInterface / value;

	private static T Sin(T value) => value switch
	{
		double r => T.CreateTruncating(Math.Sin(r)),
		decimal d => T.CreateTruncating(MathM.Sin(d)),
		LongReal lr => T.CreateTruncating(LongReal.Sin(lr)),
		LongDecimal ld => T.CreateTruncating(LongDecimal.Sin(ld)),
		_ => throw new InvalidCastException("Поддерживаются типы double, decimal, "
			+ nameof(LongReal) + " и " + nameof(LongDecimal) + '.'),
	};

	public static TSelf SinInterface(TSelf value)
	{
		var a = value.Real;
		var b = value.Imaginary;
		return TSelf.Creator(Sin(a) * Cosh(b), Cos(a) * Sinh(b));
	}

	private static T Sinh(T value) => value switch
	{
		double r => T.CreateTruncating(Math.Sinh(r)),
		decimal d => T.CreateTruncating(MathM.Sinh(d)),
		LongReal lr => T.CreateTruncating(LongReal.Sinh(lr)),
		LongDecimal ld => T.CreateTruncating(LongDecimal.Sinh(ld)),
		_ => throw new InvalidCastException("Поддерживаются типы double, decimal, "
			+ nameof(LongReal) + " и " + nameof(LongDecimal) + '.'),
	};

	public static TSelf SinhInterface(TSelf value)
	{
		var a = value.Real;
		var b = value.Imaginary;
		return TSelf.Creator(Sinh(a) * Cos(b), Cosh(a) * Sin(b));
	}

	private static decimal Sqrt(decimal value)
	{
		if (value < 0) throw new ArgumentOutOfRangeException(nameof(value),
			"Квадратный корень из отрицательного числа не определен.");
		var guess = MathM.Sqrt(value); // Начальное приближение
		while (true)
		{
			var previous = guess;
			guess = (previous + value / previous) / 2;
			if (Math.Abs(previous - guess) < 1e-28m) // Условие сходимости
				return guess;
		}
	}

	private static T Sqrt(T value) => value switch
	{
		double r => T.CreateTruncating(Math.Sqrt(r)),
		decimal d => T.CreateTruncating(Sqrt(d)),
		LongReal lr => T.CreateTruncating(LongReal.Sqrt(lr)),
		LongDecimal ld => T.CreateTruncating(LongDecimal.Sqrt(ld)),
		_ => throw new InvalidCastException("Поддерживаются типы double, decimal, "
			+ nameof(LongReal) + " и " + nameof(LongDecimal) + '.'),
	};

	public static TSelf SqrtInterface(TSelf value) =>
		FromPolarCoordinatesInterface(Sqrt(value.Magnitude), value.Phase / (T.One + T.One));
	public static TSelf SquareInterface(TSelf value) =>
		FromPolarCoordinatesInterface(value.Magnitude * value.Magnitude, value.Phase * (T.One + T.One));
	public static TSelf TanInterface(TSelf value) => SinInterface(value) / CosInterface(value);
	public static TSelf TanhInterface(TSelf value) => SinhInterface(value) / CoshInterface(value);
	public string ToStringInterface(string? format) =>
		'(' + Real.ToString(format, CultureInfo.CurrentCulture) + ", "
		+ Imaginary.ToString(format, CultureInfo.CurrentCulture) + ')';
	public string ToStringInterface(IFormatProvider? provider) =>
		'(' + Real.ToString(null, provider) + ", " + Imaginary.ToString(null, provider) + ')';
	public string ToStringInterface() => string.Format(CultureInfo.CurrentCulture, "({0}, {1})", Real, Imaginary);
	public string ToStringInterface(string? format, IFormatProvider? provider) =>
		'(' + Real.ToString(format, provider) + ", " + Imaginary.ToString(format, provider) + ')';

	public static TSelf operator -(IComplexNumber<T, TSelf> value) => TSelf.Creator(-value.Real, -value.Imaginary);
	public static TSelf operator +(IComplexNumber<T, TSelf> left, IComplexNumber<T, TSelf> right) =>
		TSelf.Creator(left.Real + right.Real, left.Imaginary + right.Imaginary);
	public static TSelf operator -(IComplexNumber<T, TSelf> left, IComplexNumber<T, TSelf> right) =>
		TSelf.Creator(left.Real - right.Real, left.Imaginary - right.Imaginary);

	public static TSelf operator *(IComplexNumber<T, TSelf> left, IComplexNumber<T, TSelf> right)
	{
		// Multiplication:  (a + bi)(c + di) = (ac -bd) + (bc + ad)i
		var result_Realpart = left.Real * right.Real - left.Imaginary * right.Imaginary;
		var result_Imaginarypart = left.Imaginary * right.Real + left.Real * right.Imaginary;
		return TSelf.Creator(result_Realpart, result_Imaginarypart);
	}

	public static TSelf operator /(IComplexNumber<T, TSelf> left, T right) =>
		TSelf.Creator(left.Real / right, left.Imaginary / right);

	public static TSelf operator /(IComplexNumber<T, TSelf> left, IComplexNumber<T, TSelf> right)
	{
		// Division : Smith's formula.
		var a = left.Real;
		var b = left.Imaginary;
		var c = right.Real;
		var d = right.Imaginary;
		if (Abs(d) < Abs(c))
		{
			var doc = d / c;
			return TSelf.Creator((a + b * doc) / (c + d * doc), (b - a * doc) / (c + d * doc));
		}
		else
		{
			var cod = c / d;
			return TSelf.Creator((b + a * cod) / (d + c * cod), (-a + b * cod) / (d + c * cod));
		}
	}

	public static virtual bool operator ==(TSelf left, TSelf right) =>
		left.Real == right.Real && left.Imaginary == right.Imaginary;
	public static virtual bool operator !=(TSelf left, TSelf right) =>
		left.Real != right.Real || left.Imaginary != right.Imaginary;
}
