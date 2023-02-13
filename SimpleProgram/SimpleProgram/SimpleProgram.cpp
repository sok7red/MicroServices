#include <iostream>
#include <vector>
#include <sstream>
using namespace std;

// function prototype
//void reverse(const string& a);
string reverseString(string a);
std::vector<string> SplitSentence(const string& str);
string reverseStringUsingSplit(string str);
static int myStaticInt = 1;

void StaticVsConst(const int myValue);

int main()
{
    // function call
    //reverse(str);
    string str = "Hello World This is Theo";
    //reverseString(str);
    cout << reverseStringUsingSplit(str);
    StaticVsConst(4);

    return 0;
}

void StaticVsConst(const int myValue)
{
    cout << "\n" << myStaticInt << "\n";
    cout << myValue << "\n";

    myStaticInt += 1;
    cout << myStaticInt << "\n";
}

// function definition
std::vector<string> SplitSentence(const string& str)
{
    int j = 0;

    std::vector<string> v;
    
    for (int i = 0; i < str.length(); i++) 
    {        
        // If a space is encountered
        if (str[i] == ' ') {
            v.push_back(str.substr(j, i-j));

            // Update the starting index
            // for next word to reverse
            j = i + 1;
        }
        else if(i == str.length()-1 && j != i)
        {
            v.push_back(str.substr(j, i));
        }
    }

    return v;
}

string reverseStringUsingSplit(string str)
{
    string returnedString;
    std::stringstream ss;
    std::vector<string> stringArray = SplitSentence(str);

    for (size_t i = 0; i < stringArray.size(); i++)
    {
        ss << stringArray[stringArray.size()-i-1];
        ss << " ";
    }    
    
    returnedString = ss.str();
    returnedString.pop_back();

    return returnedString;
}


string reverseString(string str)
{
    // Reverse str using inbuilt function
    reverse(str.begin(), str.end());
    
    // Add space at the end so that the
    // last word is also reversed
    str.insert(str.end(), ' ');

    int n = str.length();

    int j = 0;

    // Find spaces and reverse all words
    // before that
    for (int i = 0; i < n; i++) {

        // If a space is encountered
        if (str[i] == ' ') {
            reverse(str.begin() + j,
                str.begin() + i);

            // Update the starting index
            // for next word to reverse
            j = i + 1;
        }
    }

    // Remove spaces from the end of the
    // word that we appended
    str.pop_back();

    // Return the reversed string
    return str;
}

