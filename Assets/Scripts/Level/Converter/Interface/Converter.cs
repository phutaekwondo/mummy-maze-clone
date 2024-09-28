public interface Converter<T1, T2>
{
    T2 Convert(T1 source);
}