# 🧠 Personalized Integrated Learning Platform for Optimized Exam Preparation

### 🚀 Overview

The **Personalized Integrated Learning Platform** is an intelligent web application that helps students **prepare for exams efficiently** using automation and AI.
It combines **smart study scheduling**, **OCR-based content extraction**, and **OpenAI-powered NLP** to generate reviewers and quizzes that are tailored to each student’s study needs.

---

## ✨ Key Features

### 📅 1. Exam & Study Session Scheduling

* Add and manage **subjects**, **class schedules**, and **exam dates**.
* Automatically generate **personalized study sessions** based on:

  * Exam **priority level** (High / Medium / Low)
  * **Available study times** (morning, afternoon, evening)
  * **Existing subject schedules**
* Displays sessions and schedules in an **interactive calendar** interface.

---

### 📄 2. File Upload + OCR Integration

* Upload **PDF files** (reviewers, notes, lecture slides, etc.).
* Uses **Syncfusion OCR Processor** to:

  * Convert PDFs into images
  * Perform OCR text extraction
  * Handle large files (up to **100MB**)
* Extracted text is passed to the NLP engine for reviewer generation.

---

### 🧠 3. Reviewer Generation (OpenAI NLP)

* The extracted text is processed using **OpenAI’s NLP models**.
* Automatically generates:

  * **Concise summaries**
  * **Key topic highlights**
  * **Readable reviewer content** optimized for quick studying.

---

### 📝 4. Automatic Quiz Generation

* Quizzes are generated from the reviewer content using **OpenAI NLP**.
* Supported quiz formats include:

  * Multiple Choice
  * True/False
  * Fill in the Blank
  * Matching Type
* Provides quick, topic-based assessments directly from uploaded materials.

---

### 🔍 5. Related Literature Suggestions

* The system uses extracted content and **OpenAI embeddings** to suggest **relevant articles, papers, and learning resources** for deeper understanding.

---

## 🧮 Study Session Guidelines

| **Priority** | **Study Duration**    | **Daily Study Time** | **Total Hours** | **Break Schedule**        |
| ------------ | --------------------- | -------------------- | --------------- | ------------------------- |
| High         | 3–4 weeks before exam | 2–3 hrs/day          | 40–60 hrs       | 1 hr study → 15 min break |
| Medium       | 2–3 weeks before exam | 1–2 hrs/day          | 20–40 hrs       | 1 hr study → 15 min break |
| Low          | 1–2 weeks before exam | 1 hr/day             | 7–14 hrs        | 1 hr study → 15 min break |

---

## ⚙️ Technology Stack

### 🖥️ Frontend

* **Blazor WebAssembly (.NET 8)**
* **Bootstrap 5** for styling and layout
* **Syncfusion UI Components** (Calendar, Dialogs, File Upload, etc.)

### ⚙️ Backend

* **ASP.NET Core 8.0 API**
* **Entity Framework Core** for ORM and data management
* **Dependency Injection** for modular service structure
* **OpenAI API** for NLP text processing

### 🧩 Database

* **SQL Server**

### 🤖 AI Integration

| Feature             | Library/Service          |
| ------------------- | ------------------------ |
| OCR                 | Syncfusion OCR Processor |
| NLP                 | OpenAI GPT-based Models  |
| Reviewer Generation | OpenAI                   |
| Quiz Generation     | OpenAI                   |

---

## 🔮 Future Enhancements

* 🔐 User authentication & profiles
* ☁️ Cloud storage for uploaded files
* 📊 Analytics and progress tracking dashboard
* 📱 PWA support for mobile devices
* 🧭 Adaptive AI-driven study plan recommendations

---

## 👨‍💻 Contributors

* **Lead Developer:** John Lynzee Plaza
* **Framework:** .NET 8.0 with Blazor WebAssembly
* **Styling:** Bootstrap 5
* **OCR Integration:** Syncfusion OCR Processor
* **NLP Integration:** OpenAI GPT-based Models
* **Database:** Microsoft SQL Server
