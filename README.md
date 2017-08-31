# resume-generator

A program to help generate html resumes and cover letters. It allows you to save lists of skills and job experience with achievements and then allows you to select from those lists to tailor your resume to the job you're applying for.

## version: 0.1

version 0.1 contains none of that, and is simply working towards being able to combine hand generated source files into the finished product, but the intro description is what we're working towards.

## Using version 0.1

While the eventual goal is to create a gui to manufacture the source files used to compile the finished resume, at the moment you'll still have to provide these assets by hand. The following files are expected in the directory.

	html
		raw.html
		_about.html
		_education.html
		_cover.html
		_order.html
		_name.html
	xml
		contact.xml
		greeting.xml
		skills.xml
		experience.xml
		links.xml
	other
		style.css
		scripts.js


After the required files are created you'll need to pass the directory as an argument to the program and it will generate the final html resume. Your can also pass in a file name for the output as a second argument. 

### raw.html

The raw html file, named **raw.html**, contains the boiler plate html and insert tags. The raw html file must contain the following tags: 

	<!--INSERT SCRIPT-->
	<!--INSERT STYLE-->
	<!--INSERT COVER-->
	<!--INSERT CONTACT-->
	<!--INSERT NAME-->
	<!--INSERT GREETING-->
	<!--INSERT LINKS-->
	<!--INSERT SECTIONS-->
	
Other than that requirement the document can be whatever you want.

### order.html

The order file contains the insert tags for the resume sections in the order that you indend them to be in the document. 

	<!--INSERT ABOUT-->
	<!--INSERT SKILLS-->
	<!--INSERT XP-->
	<!--INSERT EDUCATION-->
	
Beyond verifying that each of the tags is present the text is inserted as is. You can therefore add additional html in this document if needed.

example:

	<div>
		<!--INSERT ABOUT-->
		<hr />
		<!--INSERT SKILLS-->
		<hr />
		<!--INSERT XP-->
		<hr />
		<!--INSERT EDUCATION-->
	</div>

### style.css

This is a standard css document.

### scripts.js

This is a standard javascript document.

### cover.html

This should be the content of you cover letter minus greeting and contact. (all of which will be inserted later)

### contact.xml

This is your contact information. It is converted into a div with the id contact as an unordered list. The root node of the xml document is **contact**. The allowed sub elements are:

- us-address
	- street
	- po-box
	- city
	- state
	- zipcode
- phone
	- number
	- type
- email
- other

<hr />

**example input:**

	<contact>
		<us-address>
			<street>555 Madeup St.</street>
			<city>Lincoln</city>
			<state>NE</state>
			<zipcode>68588</zipcode>
		<us-address>
		<phone>
			<number>1.555.555.1111</number>
			<type>Home</type>
		</phone>
		<email>example@example.com</email>
	</contact>

**example output:**

	<div class="contact">
		<ul>
			<li>555 Madeup St.</li>
			<li>Lincoln, NE 68588</li>
			<li>Home : 1.555.555.1111</li>
			<li>example@example.com</li>
		</ul>
	</div>


### greeting.xml

This is the contact information and greeting line of the cover letter. The root node is **greeting**. 
The sub elements allowed are:

- name **(required)**
- us-address
	- street
	- city
	- state/province
	- zipcode
- salutation **(required)**

**example input:**

	<greeting>
		<name>Awesome Company Name</name>
		<us-address>
			<street>556 Madeup St.</street>
			<city>Lincoln</city>
			<state>NE</state>
			<zipcode>68588</zipcode>
		</us-address>
		<salutation>Dear Hiring Manager;</salutation>
	</greeting>
	
**example output**

	<div id="greeting">
		<div id="company-contact">
			<ul>
				<li>Awesome Company Name</li>
				<li>556 Madeup St.</li>
				<li>LIncoln, NE 68588</li>
			</ul>
		</div>
		<div id="salutation">
			<div>Dear Hiring Manager;</div>
		</div>
	</div>

### about.html

The about section is a simple html section allowing you to write an objective, short bio, or whatever else you would like. If you would like to skip this section the best option would be to just include an html comment as the content.

### skills.xml

The skills you wish to highlight. It allows for some additional flavor attributes that are best used with styling that makes the elements hoverable or collapseable. The root node needs to be **skills** and each skill is encapsulated in a **skill** element. The allowable sub elements include:

- name **required**
- category **required**
- percentage
- mastery
- comment

**example input:**

	<skills>
		<skill>
			<name>C#</name>
			<category>Technical Skills</category>
			<percentage>20</percentage>
			<mastery>Beginner</mastery>
		</skill>
		<skill>
			<name>Javascript</name>
			<category>Technical Skills</category>
			<percentage>75</percentage>
			<mastery>Advanced</mastery>
			<comments>Expereince working with JQuery, JSON and Ajax</comments>
		</skill>
		<skill>
			<name>Monodevelop</name>
			<category>Tools</category>
			<percentage>25</percentage>
			<mastery>Beginner</mastery>
		</skill>
	</skills>
	
**example output:**
	
	<div id="skills">
		<div class="skill-category">
			<div class="skill-category-title">Technical Skills</div>
			<div class="skill">
				<div class="skill-name">C#</div>
				<div class="skill-percentage-bar">
					<div style="width: 20%;"></div>
				</div>
				<div class="skill-mastery">Beginner</div>
			</div>
			<div class="skill">
				<div class="skill-name">Javascript</div>
				<div class="skill-percentage-bar">
					<div style="width: 75%;"></div>
				</div>
				<div class="skill-mastery">Advanced</div>
				<div class="skill-comment">Expereince working with JQuery, JSON and Ajax</div>
			</div>
		</div>
		<div class="skill-category">
			<div class="skill-category-title">Tools</div>
			<div class="skill">
				<div class="skill-name">Monodevelop</div>
				<div class="skill-percentage-bar">
					<div style="width: 25%;"></div>
				</div>
				<div class="skill-mastery">Beginner</div>
			</div>
		</div>
	</div>
	
### experience.xml

The experience document uses **xp** as the root element and any jobs you wish to list are placed in a **job** element. The following is the list of elements allowed:

- title **(required)**
- company **(required)**
- dates
- point *(mulitple)*

**example input:**

	<xp>
		<job>
			<title>Attendant</title>
			<company>The Local Gas Station</company>
			<dates>2015-2016</dates>
		</job>
		<job>
			<title>General IT</title>
			<company>The Local NPO</company>
			<dates>2016-present</dates>
			<point>That one great thing you did</point>
			<point>The other equally great thing you did</point>
		</job>
	</xp>
	
**example output**

	<div id="xp">
		<div class="job">
			<div class="job-header">
				<div class="job-title">Attendant</div>
				<div class="job-company">The Local Gas Station</div>
				<div class="job-dates">2015-2016</div>
			</div>
		</div>
		<div class="job">
			<div class="job-header">
				<div class="job-title">General IT</div>
				<div class="job-company">The Local NPO</div>
				<div class="job-dates">2016-present</div>
			</div>
			<ul>
				<li>That one great thing you did</li>
				<li>The other equally great thing you did</li>
			</ul>
		</div>
	</div>

### education.html

The education section is a simple html section allowing you to write your education history/certifications. If you would like to skip this section the best option would be to just include an html comment as the content.

### links.xml

A list of links you'd like included to outside profiles etc. The root element is **links** and each link is encapsulted in a **link** element containing two subelements **text** and **url**.

**example:**

	<links>
		<link>
			<text>Github Profile</text>
			<url>https://www.github.com/tajahem</url>
		</link>
		<link>
			<text>DeviantArt Profile</text>
			<url>https://www.deviantart.com/tajahem</url>
		</link>
	</links>