using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeardHarder.Core
{
    public static class RegexPatterns
    {
        public static String[] EpisodeRegexes = new[] {

            @"^(?<series_name>.+?)[. _-]+" +  
            @"s(?<season_num>[0-9]+)[. _-]?" +              
            @"e(?<ep_num>[0-9]+)",   

            @"^(?<series_name>.+?)[. _-]+" +          
            @"s(?<season_num>\d+)[. _-]*" +              
            @"e(?<ep_num>\d+)" +                         
            @"([. _-]+s(?=season_num)[. _-]*" +          
            @"e(?<extra_ep_num>\d+))+" +                 
            @"[. _-]*((?<extra_info>.+?)" +              
            @"((?<![. _-])(?<!WEB)" +                     
            @"-(?<release_group>[^- ]+))?)?$",           
        
            @"^(?<series_name>.+?)[. _-]+" +               
            @"(?<season_num>\d+)x" +                       
            @"(?<ep_num>\d+)" +                            
            @"([. _-]+(?=season_num)x" +                   
            @"(?<extra_ep_num>\d+))+" +                    
            @"[. _-]*((?<extra_info>.+?)" +                
            @"((?<![. _-])(?<!WEB)" +                       
            @"-(?<release_group>[^- ]+))?)?$",            
              
            @"^((?<series_name>.+?)[. _-]+)?" +            
            @"s(?<season_num>\d+)[. _-]*" +                
            @"e(?<ep_num>\d+)" +                           
            @"(([. _-]*e|-)" +                              
            @"(?<extra_ep_num>(?!(1080|720)[pi])\d+))*" +  
            @"[. _-]*((?<extra_info>.+?)" +                
            @"((?<![. _-])(?<!WEB)" +                       
            @"-(?<release_group>[^- ]+))?)?$",          

            @"^((?<series_name>.+?)[\[. _-]+)?" +         
            @"(?<season_num>\d+)x" +                      
            @"(?<ep_num>\d+)" +                           
            @"(([. _-]*x|-)" +                             
            @"(?<extra_ep_num>" +
            @"(?!(1080|720)[pi])(?!(?<=x)264)" +           
            @"\d+))*" +                                    
            @"[\]. _-]*((?<extra_info>.+?)" +             
            @"((?<![. _-])(?<!WEB)" +                      
            @"-(?<release_group>[^- ]+))?)?$",            
        
            @"^((?<series_name>.+?)[. _-]+)?" +           
            @"(?<air_year>\d{4})[. _-]+" +                
            @"(?<air_month>\d{2})[. _-]+" +               
            @"(?<air_day>\d{2})" +                        
            @"[. _-]*((?<extra_info>.+?)" +               
            @"((?<![. _-])(?<!WEB)" +                      
            @"-(?<release_group>[^- ]+))?)?$",            
              
            @"(?<release_group>.+?)-\w+?[\. ]?" +          
            @"(?!264)" +                                    
            @"(?<season_num>\d{1,2})" +                    
            @"(?<ep_num>\d{2})$",                          
              
            @"^(?<series_name>.+?)[. _-]+" +               
            @"season[. _-]+" +                              
            @"(?<season_num>\d+)[. _-]+" +                 
            @"episode[. _-]+" +                             
            @"(?<ep_num>\d+)[. _-]+" +                     
            @"(?<extra_info>.+)$",                        
              
            @"^((?<series_name>.+?)[. _-]+)?" +            
            @"s(eason[. _-])?" +                            
            @"(?<season_num>\d+)[. _-]*" +                 
            @"[. _-]*((?<extra_info>.+?)" +                
            @"((?<![. _-])(?<!WEB)" +                       
            @"-(?<release_group>[^- ]+))?)?$",             

            @"^((?<series_name>.+?)[. _-]+)?" +            
            @"(e(p(isode)?)?|part|pt)[. _-]?" +             
            @"(?<ep_num>(\d+|[ivx]+))" +                   
            @"((([. _-]+(and|&|to)[. _-]+)|-)" +            
            @"(?<extra_ep_num>(?!(1080|720)[pi])(\d+|[ivx]+))[. _-])" + 
            @"([. _-]*(?<extra_info>.+?)" +               
            @"((?<![. _-])(?<!WEB)" +                      
            @"-(?<release_group>[^- ]+))?)?$",            

            @"^((?<series_name>.+?)[. _-]+)?" +         
            @"(e(p(isode)?)?|part|pt)[. _-]?" +          
            @"(?<ep_num>(\d+|([ivx]+(?=[. _-]))))" +    
            @"([. _-]+((and|&|to)[. _-]+)?" +            
            @"((e(p(isode)?)?|part|pt)[. _-]?)" +        
            @"(?<extra_ep_num>(?!(1080|720)[pi])" +
            @"(\d+|([ivx]+(?=[. _-]))))[. _-])*" +       
            @"([. _-]*(?<extra_info>.+?)" +             
            @"((?<![. _-])(?<!WEB)" +                    
            @"-(?<release_group>[^- ]+))?)?$",          

            @"^(?<series_name>.+?)[. _-]+" +               
            @"(?<season_num>\d{1,2})" +                    
            @"(?<ep_num>\d{2})" +                          
            @"([. _-]+(?<extra_info>(?!\d{3}[. _-]+)[^-]+)" + 
            @"(-(?<release_group>.+))?)?$",                
              
            @"^((?<series_name>.+?)(?:[. _-]{2,}|[. _]))?" + 
            @"(?<ep_num>\d{1,2})" +                          
            @"(?:-(?<extra_ep_num>\d{1,2}))*" +          
            @"[. _-]+((?<extra_info>.+?)" +              
            @"((?<![. _-])(?<!WEB)" +                     
            @"-(?<release_group>[^- ]+))?)?$"          

        };
    }
}
