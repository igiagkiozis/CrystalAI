The files in this directory were generated using the following code. If you would like to verify the
correctness of PCG and PCGExtended implementation, download the PCG C++ library from here http://www.pcg-random.org/download.html
and re-create all the cases you see in the Data/ folder using the code below. If you change the 
seed (42) don't forget to also change the seed in the tests. 

#include <iostream>
#include <fstream>
#include <iomanip>
#include <string>
#include <map>
#include <random>
#include <cmath>

#include "pcg_random.hpp"

int main()
{
    // This was used to generate the test files in the Data/ folder. "pcg_random.hpp" can be downloaded
    // from O'Neill's website: http://www.pcg-random.org/download.html
    int seed = 42;
    const pcg_detail::bitcount_t mtable_pow2 = 10; // This controls the equidistribution
    // This has 2 values in the reference code 16 and 32, although any value that is greater or equal
    // than mtable_pow2 and smaller or equal to 32 (for 32-bit generators) should be fine. The larger
    // this value is the less often the internal table is advanced, which can make the generator
    // faster. In my epxeriments I didn't see any dramatic changes in performance, so I would prefer 
    // a value of 16 for this, but to each his own. 
    const pcg_detail::bitcount_t madvance_pow2 = 32;
    pcg_engines::ext_setseq_xsh_rr_64_32<mtable_pow2,madvance_pow2,true> rng(seed);
    
    int N = 10000;
    std::ofstream myfile;
    std::string filename("/!CHANGE_ME!/pcg32_k");
    filename += "_table_pow2_" + std::to_string(mtable_pow2);
    filename += "_advance_pow2_" + std::to_string(madvance_pow2);
    filename += "_seed_" + std::to_string(seed) + ".txt";
    myfile.open(filename);
    for (int n = 0; n < N; ++n) {
        unsigned int v = rng();
        myfile << v << "\n";
    }
    myfile.close();
}

