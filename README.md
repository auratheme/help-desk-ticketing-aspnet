# Solvi: Help Desk Ticketing System in Asp.Net Core Mvc
A complete Help Desk Ticketing System with a Knowledge Base built using ASP.NET Core MVC, C#, and .NET 8. Designed for managing support tickets efficiently. Fully customizable and ideal for businesses of all sizes looking for a robust ticketing solution.

# Free Version Features
- Feel free to download from Github
- Try Live demo: http://auratheme-001-site9.ntempurl.com/
- User Roles: Admin, Customer
- Customer can sign-up an account and submit support tickets
- All admin can see all the tickets and reply to the tickets
- Support ticket module
- Login history
- Email notifications to the customer when the ticket has new reply from admin
- Ticket status
- Close ticket
- Delete ticket

# How to Run the Project Locally
Follow these steps to set up and run the project on your local machine:
1. Download the Project
2. Configure the Connection String
    - Open the appsettings.json file located in the root of the project.
    - Replace the DefaultConnection value in the ConnectionStrings section with your SQL Server connection string.
    - You can also refer to this video: https://youtu.be/WjLV5ccuzaU?si=vxUw21FNxlWFDDoc&t=172 (From 2.52 to 3.47)
3. Create the Database
   - In Visual Studio, click Tools > NuGet Package Manager > Package Manager Console.
   - Run the following command:
        - Update-Database
    - You can also refer to this video: https://youtu.be/WjLV5ccuzaU?si=vrtD40cNlk4vO3U6&t=228 (From 3.48 to 4.55)
4. Run the Project
   - Press CTRL + F5 to run the project. Click the sign-up button at the top right corner. Register the first admin account.

# Premium Version Features:
- Live demo: http://auratheme-001-site8.ntempurl.com/
- Video demo: https://www.youtube.com/watch?v=NDcW633SVeQ
- Product page: https://auratheme.gumroad.com/l/solvi-ticket-helpdesk-asp-net-mvc

## The following are Solvi Premium Version Full Features:

### User Roles
- Admin
- Agent
- Customer

### General
- Multilingual Support
    - Easily switch between languages to suit user preferences.
- Light and Dark Mode
    - Choose between light and dark themes for an optimal viewing experience.
- RTL Support
    - Fully supports right-to-left (RTL) languages, such as Arabic.
- User Management:
    - Register and create an account.
    - Login securely to access features.
    - Recover access with a "Forgot Password" option.
    - Change your password anytime for added security.
    - Edit and update profile details with ease.
- Flexible Layout Options:
    - Customize the interface with sidebar, icon bar, or top bar layouts.
- Interactive Dashboard:
    - Visualize data with charts and tables for better insights.
- Responsive Design:
    - Fully mobile-friendly, ensuring a seamless experience on any device.

### Knowledge Base
- Categories management (by admin)
- Articles management (by admin)
- Custom Article URL, for SEO
- Bulk import categories from excel (by admin)
- Bulk import articles from excel (by admin)
- Export to excel (by admin)
- Accessible to all users, whether logged in or not
- AI features:
    - Enter an article title and get article body suggestion from AI
    - Enter an article title and get tags suggestion from AI
- User-friendly interface for easy navigation
- Content categorization and tagging
- Category management, by admin
- Article management, by admin
- SEO-friendly custom article URL
- Bulk categories import from Excel
- Bulk articles import from Excel
- Excel export capabilities
- Public access for all visitors
- Content organization with tagging capability
- Search functionality
- Article rating and feedback system that helps administrators improve content and understand user needs
- Analytics and reporting on article usage
- Mobile accessibility and responsive design

### Support Ticket
- Ticket creation and submission
- Admin ticket assignment
- Priority levels for tickets
- Ticket tagging
- Status tracking: Open, In Progress, Closed
- Rich text editor
    - Text formatting options
    - Image pasting in message editor
- File attachments: Single file or ZIP archive containing multiple files
- AI features:
    - Get AI-generated reply suggestions
    - Convert closed tickets into knowledge base articles
- Saved replies library: Store common responses for quick access when replying to customers
- Automatic email notifications
- Real-time SignalR notification bell
- Customizable ticket fields: Priority, request type, impact, urgency
- Customizable ticket status labels: Change from Open, In Progress, Closed to other labels
- Print a ticket conversation to PDF
- Export ticket title, creation date, creator name, and status into Excel, Csv, Docx, Pdf, and Txt
- Search ticket functionality
- Filter ticket month, year, status, agent assignment

### Handle Different Ticket Workflows and Scenarios
**Scenario 1: Standard Ticket Process**
1. Customer creates and submits a ticket
2. Admin receives email and bell notifications
3. Admin assigns ticket to an agent
4. Agent receives email and bell notifications
5. Agent logs in to access the ticket details and responds to the customer
6. Customer receives a notification when the agent replies
7. Customer logs in to continue the conversation with the agent
8. Either customer or agent can close the ticket once it's resolved
**Scenario 2: External Customer Contact via Email or WhatsApp**
1. Company receives an email or WhatsApp message from a customer
2. Admin or agent sends a customer registration link for helpdesk system access
3. Customer registers an account through the registration link
4. Agent and customer continue their communication through the ticket system
**Scenario 3: Recording Historical Support Cases**
1. Admin or agent retrieves the past support cases from email or WhatsApp or other platforms
2. Admin or agent creates new tickets
3. Admin or agent adds the previous email or WhatsApp conversation to the ticket
    - Ensures "Send email to notify the customer" checkbox remains unchecked
4. After entering the complete conversation history, close the ticket

### AI Chatbot
- Accessible to all users publicly.
- Instantly answers questions using AI-powered automation.
- Leverages knowledge base articles marked with "Use in AI chatbot" to provide responses.
- Export chat histories in various formats: Excel, CSV, PDF, and DOCX.
- Customize the chatbot title.
- Delete chat histories as needed.
- Admins can set default questions for the chatbot.
- Change the chatbot's avatar to match your brand or preferences.

### Logging
- Email logs (Accessible by admin)
- Notification logs (Accessible by admin)
- Error logs (Accessible by admin)
- Login histories (Accessible by all users)

### More features will come in the premium version! Stay tuned!

