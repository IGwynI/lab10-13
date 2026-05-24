using System;

namespace Компилятор
{
    class LexicalAnalyzer
    {
        public const byte star = 21;
        public const byte slash = 60;
        public const byte equal = 16;
        public const byte comma = 20;
        public const byte semicolon = 14;
        public const byte colon = 5;
        public const byte point = 61;
        public const byte arrow = 62;
        public const byte leftpar = 9;
        public const byte rightpar = 4;
        public const byte lbracket = 11;
        public const byte rbracket = 12;
        public const byte flpar = 63;
        public const byte frpar = 64;
        public const byte later = 65;
        public const byte greater = 66;
        public const byte laterequal = 67;
        public const byte greaterequal = 68;
        public const byte latergreater = 69;
        public const byte plus = 70;
        public const byte minus = 71;
        public const byte assign = 51;
        public const byte twopoints = 74;
        public const byte ident = 2;
        public const byte intc = 15;
        public const byte casesy = 31;
        public const byte elsesy = 32;
        public const byte filesy = 57;
        public const byte gotosy = 33;
        public const byte thensy = 52;
        public const byte typesy = 34;
        public const byte untilsy = 53;
        public const byte dosy = 54;
        public const byte withsy = 37;
        public const byte ifsy = 56;
        public const byte insy = 100;
        public const byte ofsy = 101;
        public const byte orsy = 102;
        public const byte tosy = 103;
        public const byte endsy = 104;
        public const byte varsy = 105;
        public const byte divsy = 106;
        public const byte andsy = 107;
        public const byte notsy = 108;
        public const byte forsy = 109;
        public const byte modsy = 110;
        public const byte nilsy = 111;
        public const byte setsy = 112;
        public const byte beginsy = 113;
        public const byte whilesy = 114;
        public const byte arraysy = 115;
        public const byte constsy = 116;
        public const byte labelsy = 117;
        public const byte downtosy = 118;
        public const byte packedsy = 119;
        public const byte recordsy = 120;
        public const byte repeatsy = 121;
        public const byte programsy = 122;
        public const byte functionsy = 123;
        public const byte procedurensy = 124;

        private byte _symbol;
        private TextPosition _token;
        private string _addrName;
        private int _nmb_int;

        public byte Symbol
        {
            get 
            { 
                return _symbol; 
            }
            set 
            { 
                _symbol = value;
            }
        }

        public TextPosition Token
        {
            get 
            { 
                return _token; 
            }
            set 
            { 
                _token = value; 
            }
        }

        public string AddrName
        {
            get 
            { 
                return _addrName; 
            }
            set 
            { 
                _addrName = value; 
            }
        }

        public int NmbInt
        {
            get 
            { 
                return _nmb_int; 
            }
            set 
            { 
                _nmb_int = value; 
            }
        }

        public byte NextSym()
        {
            string name;
            byte code;
            int digit;
            const int maxint = Int16.MaxValue;

            while (!InputOutput.EndOfFile && InputOutput.Ch == ' ')
            {
                InputOutput.NextCh();
            }

            if (InputOutput.EndOfFile)
            {
                return 0;
            }

            _token = InputOutput.PositionNow;


            if (char.IsLetter(InputOutput.Ch) || InputOutput.Ch == '_')
            {
                name = "";
                while (!InputOutput.EndOfFile &&
                       (char.IsLetterOrDigit(InputOutput.Ch) || InputOutput.Ch == '_'))
                {
                    name += InputOutput.Ch;
                    InputOutput.NextCh();
                }
                code = Keywords.GetCode(name.ToLower());
                if (code != 0)
                {
                    _symbol = code;
                }
                else
                {
                    _symbol = ident;
                    _addrName = name;
                }
                return _symbol;
            }


            if (char.IsDigit(InputOutput.Ch))
            {
                _nmb_int = 0;
                while (!InputOutput.EndOfFile && char.IsDigit(InputOutput.Ch))
                {
                    digit = InputOutput.Ch - '0';
                    if (_nmb_int > (maxint - digit) / 10)
                    {
                        InputOutput.Error(203, InputOutput.PositionNow);
                        while (!InputOutput.EndOfFile && char.IsDigit(InputOutput.Ch))
                        {
                            InputOutput.NextCh();
                        }
                        _nmb_int = 0;
                        break;
                    }
                    _nmb_int = _nmb_int * 10 + digit;
                    InputOutput.NextCh();
                }
                _symbol = intc;
                return intc;
            }


            switch (InputOutput.Ch)
            {
                case ',':
                    _symbol = comma;
                    InputOutput.NextCh();
                    break;
                case ';':
                    _symbol = semicolon;
                    InputOutput.NextCh();
                    break;
                case '+':
                    _symbol = plus;
                    InputOutput.NextCh();
                    break;
                case '-':
                    _symbol = minus;
                    InputOutput.NextCh();
                    break;
                case '=':
                    _symbol = equal;
                    InputOutput.NextCh();
                    break;
                case '[':
                    _symbol = lbracket;
                    InputOutput.NextCh();
                    break;
                case ']':
                    _symbol = rbracket;
                    InputOutput.NextCh();
                    break;
                case '^':
                    _symbol = arrow;
                    InputOutput.NextCh();
                    break;
                case '/':
                    _symbol = slash;
                    InputOutput.NextCh();
                    break;

                case '(':
                    InputOutput.NextCh();
                    if (!InputOutput.EndOfFile && InputOutput.Ch == '*')
                    {
                        InputOutput.NextCh();
                        while (!InputOutput.EndOfFile)
                        {
                            if (InputOutput.Ch == '*')
                            {
                                InputOutput.NextCh();
                                if (!InputOutput.EndOfFile && InputOutput.Ch == ')')
                                {
                                    InputOutput.NextCh();
                                    break;
                                }
                            }
                            else
                            {
                                InputOutput.NextCh();
                            }
                        }
                        return NextSym();
                    }
                    else
                    {
                        _symbol = leftpar;
                    }
                    break;

                case ')':
                    _symbol = rightpar;
                    InputOutput.NextCh();
                    break;

                case '*':
                    _symbol = star;
                    InputOutput.NextCh();
                    break;

                case ':':
                    InputOutput.NextCh();
                    if (!InputOutput.EndOfFile && InputOutput.Ch == '=')
                    {
                        _symbol = assign;
                        InputOutput.NextCh();
                    }
                    else
                    {
                        _symbol = colon;
                    }
                    break;

                case '<':
                    InputOutput.NextCh();
                    if (!InputOutput.EndOfFile && InputOutput.Ch == '=')
                    {
                        _symbol = laterequal;
                        InputOutput.NextCh();
                    }
                    else if (!InputOutput.EndOfFile && InputOutput.Ch == '>')
                    {
                        _symbol = latergreater;
                        InputOutput.NextCh();
                    }
                    else
                    {
                        _symbol = later;
                    }
                    break;

                case '>':
                    InputOutput.NextCh();
                    if (!InputOutput.EndOfFile && InputOutput.Ch == '=')
                    {
                        _symbol = greaterequal;
                        InputOutput.NextCh();
                    }
                    else
                    {
                        _symbol = greater;
                    }
                    break;

                case '.':
                    InputOutput.NextCh();
                    if (!InputOutput.EndOfFile && InputOutput.Ch == '.')
                    {
                        _symbol = twopoints;
                        InputOutput.NextCh();
                    }
                    else
                    {
                        _symbol = point;
                    }
                    break;

                case '{':
                    InputOutput.NextCh();
                    while (!InputOutput.EndOfFile && InputOutput.Ch != '}')
                    {
                        InputOutput.NextCh();
                    }
                    if (InputOutput.Ch == '}')
                    {
                        InputOutput.NextCh();
                    }
                    return NextSym();

                case '}':
                    InputOutput.NextCh();
                    return NextSym();

                case '\'':
                    InputOutput.NextCh();
                    if (!InputOutput.EndOfFile)
                    {
                        InputOutput.NextCh();
                    }
                    if (!InputOutput.EndOfFile && InputOutput.Ch == '\'')
                    {
                        InputOutput.NextCh();
                    }
                    _symbol = 80;
                    break;

                default:
                    InputOutput.Error(1, InputOutput.PositionNow);
                    InputOutput.NextCh();
                    return NextSym();
            }

            return _symbol;
        }
    }
}