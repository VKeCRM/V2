//-----------------------------------------------------------------------
// <copyright file="RsaKeySize.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace VKeCRM.Common.Cryptography
{
    /// <summary>
    /// Represents supported RSA Key sizes.
    /// </summary>
    /// <remarks>
    /// Key size selection is the first important decision to be upon selection of RSA 
    /// for a cryptosystem. The size of the key actually refers to the size (in bits) 
    /// of the modulus, N, not the size of any of the public or private keys. Two 
    /// randomly selected primes, p and q, should be chosen such that they are 
    /// approximately the same length to ensure that any attempts to factor the modulus 
    /// are much more difficult. Key sizes for public-key cryptosystems should be much 
    /// larger than private-key cryptosystems and so comparisons between the two should 
    /// not be made. 
    /// <para><br/></para>
    /// The decision of the key size to be used should be based on a thorough assessment 
    /// of the security solution requirements for the cryptosystem. This entails an 
    /// evaluation of the value of the data to be protected as well as the length of 
    /// time for which it needs to be protected. A corresponding factor is also an 
    /// appraisal of who might wish to devise such an attack as well as what resources 
    /// they have available. A best guess can then be made based upon the extrapolation 
    /// of hardware advances, to hypothesize the computational time possible to break 
    /// the cryptosystem as well as the cost such a design would involve. 
    /// <para><br/></para>
    /// Increasing the key size will also cause a corresponding increase in the 
    /// computational load. A rough approximation is that doubling the length of the key 
    /// size will increase public key operations by a factor of four and private key 
    /// operations by a factor of eight. Public key operations are less sensitive to key 
    /// size increase because the public exponent can be fixed, while in private key 
    /// operations the length of the private exponent increases proportionately. 
    /// Doubling the length of the key size will also result in a ~16 factor increase in 
    /// key generation operations. 
    /// <para><br/></para>
    /// At first consideration, there might be a concern that for a fixed key length that 
    /// the number of primes available is finite and with a bound on the number of 
    /// possible primes, the set of primes may be so limited that it would be vulnerable 
    /// to exploitation. However, from the Prime Number Theorem it is known that the 
    /// number of primes less than or equal to N is asymptotic to N/lnN. Therefore, the 
    /// number of prime numbers of length 512 bits or less is roughly 10150. The set of 
    /// available primes is large enough to be effectively considered infinite. 
    /// <para><br/></para>
    /// Current implementations of attacks attempting to break RSA cryptosystems must 
    /// consider two factors. The first is obviously the number of operations required to 
    /// factor large numbers. The second factor is the actual cost, which is based upon 
    /// the cost of the hardware times the running time. Several enhancements have been 
    /// proposed to reduce the cost of implementing the General Number Field Sieve Method, 
    /// by reducing the amount of memory required. It has been estimated that by using 
    /// these enhancements, a 1024-bit key could be broken at a cost of roughly $1 
    /// billion. However, the running time for such a design would still be measured in 
    /// decades. 
    /// <para><br/></para>
    /// Predicting the security of a given key size is difficult; some assumptions 
    /// regarding the rate of processor performance and cost of hardware must be made. 
    /// In 2000, several papers were published predicting that within 10 years it would 
    /// be possible build a system for $250 million which would be able to break a 
    /// 1024-bit key in a day. This assumed that the rate of processor performance would 
    /// double every 18 months as well as the rate of improvement in factoring algorithms 
    /// would also continue. However, much higher estimates of cost and time have also 
    /// been proposed. At this point it is generally accepted that data encrypted using a 
    /// key size of 1024-bits should be safe until 2015, although for more sensitive data, 
    /// a key size of at least 2048-bits might be best.
    /// </remarks>
    public enum RsaKeySize
    {
        /// <summary>
        /// Unknown or unsupported key size.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// 512 Bits
        /// <para>Low strength</para>
        /// </summary>
        Bits512 = 512,

        /// <summary>
        /// 1,024 Bits
        /// <para>Medium strength, acceptable for most uses.</para>
        /// </summary>
        Bits1024 = 1024,

        /// <summary>
        /// 2,048 Bits
        /// <para>High strength, desired for more sensitive data.</para>
        /// </summary>
        Bits2048 = 2048,

        /// <summary>
        /// 4,096 Bits
        /// <para>Very high strength, increased time to perform cryptographic operations.</para>
        /// </summary>
        Bits4096 = 4096,

        /// <summary>
        /// 8,192 Bits
        /// <para>Ultra high strength, increased time to perform cryptographic operations.</para>
        /// </summary>
        Bits8192 = 8192,

        /// <summary>
        /// 16,384 Bits
        /// <para>Maximum strength, increased time to perform cryptographic operations.</para>
        /// </summary>
        Bits16384 = 16384
    }
}