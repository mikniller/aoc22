use std::fs::File;
use std::io::{self, BufRead};
use std::collections::HashMap;

fn main() {
    day1();
    day2();
}

fn day1() {
    let mut sum: i32 = 0;
    let mut sums: Vec<i32> = Vec::new();

    if let Ok(lines) = read_file_lines(1,false) {
        for line in lines {
            if let Ok(l) = line {
                if l.is_empty() {
                    sums.push(sum);
                    sum=0;
                } else {
                    sum = sum+l.parse::<i32>().unwrap();                
                }
            }
        }

        sums.sort_by(|a,b|b.cmp(a));
        let p1:i32 = sums.iter().take(1).sum();
        let p2:i32 = sums.iter().take(3).sum();
        

        println!("Day1: {} {}", p1,p2);
    }

}

fn day2() {
    let lookup = HashMap::from([('X',1),('Y',2),('Z',3)]);
    let score = HashMap::from( [("A X",3), ("A Y",6),("A Z",0),( "B X",0), ("B Y",3),("B Z",6),( "C X",6), ("C Y",0),("C Z",3)]);
    let translate = HashMap::from( [
        ( "A X","A Z"), ("A Y","A X"),("A Z","A Y"),
        ( "B X","B X"), ("B Y","B Y"),("B Z","B Z"),
        ( "C X","C Y"), ("C Y","C Z"),("C Z","C X") ]);


    let mut sum:i32=0;    
    let mut sum1:i32=0;    

    if let Ok(lines) = read_file_lines(2,false) {
        for line in lines {
            if let Ok(l) = line {
                let mut c:char = l.chars().nth(2).unwrap();
                
                sum += lookup.get(&c).unwrap();
                sum += score.get(&*l).unwrap();
                
                let converted = translate.get(&*l).unwrap(); 
                c = converted.chars().nth(2).unwrap();
                
                sum1 += lookup.get(&c).unwrap();
                sum1 += score.get(&*converted).unwrap();
            }
        }
    }
    println!("Day1: {} {}", sum,sum1);

}



fn read_file_lines(day:u8, is_sample:bool) -> io::Result<io::Lines<io::BufReader<File>>>  {
    let f = if is_sample {"_sample"} else {""};
    let file_name = format!("D:\\dev\\aoc\\data\\day{}{}.txt",day,f) ;
    println!("{}",file_name);

    let file = File::open(file_name)?;
    Ok(io::BufReader::new(file).lines())
}